/**
 * Notification JS
 * Shims up the Notification API
 *
 * @author Andrew Dodson
 * @website http://adodson.com/notification.js/
 *
 * Edited by Haltroy for
 * Korot Desktop Client 0.6 and later
 * github.com/haltroy/korot
 */
function makeid(length) {
	var result = '';
	var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
	var charactersLength = characters.length;
	for (var i = 0; i < length; i++) {
		result += characters.charAt(Math.floor(Math.random() * charactersLength));
	}
	return result;
}
var korotNotificationPermission = "[$]";

(function (window, document) {
	var PERMISSION_GRANTED = 'granted',
		PERMISSION_DENIED = 'denied',
		PERMISSION_UNKNOWN = 'unknown';

	var a = [], iv, i = 0;

	//
	// Swap the document.title with the notification
	//
	function swaptitle(title) {
		if (a.length === 0) {
			a = [document.title];
		}

		a.push(title);

		if (!iv) {
			iv = setInterval(function () {
				// has document.title changed externally?
				if (a.indexOf(document.title) === -1) {
					// update the default title
					a[0] = document.title;
				}

				document.title = a[++i % a.length];
			}, 1000);
		}
	}

	function swapTitleCancel() {
		// dont do any more if we haven't got anything open
		if (a.length === 0) {
			return;
		}

		// if an IE overlay is present, kill it
		if ("external" in window && "msSiteModeClearIconOverlay" in window.external) {
			window.external.msSiteModeClearIconOverlay();
		}

		clearInterval(iv);

		iv = false;
		document.title = a[0];
		a = [];
	}

	//
	// Add aevent handlers
	function addEvent(el, name, func) {
		if (name.match(" ")) {
			var a = name.split(' ');
			for (var i = 0; i < a.length; i++) {
				addEvent(el, a[i], func);
			}
		}
		if (el.addEventListener) {
			el.removeEventListener(name, func, false);
			el.addEventListener(name, func, false);
		}
		else {
			el.detachEvent('on' + name, func);
			el.attachEvent('on' + name, func);
		}
	}

	function check_permission() {
		// Check whether the current desktop supports notifications and if they are authorised,
		// PERMISSION_GRANTED (yes they are supported and permission is granted),
		// PERMISSION_DENIED (yes they are supported, permission has not been granted),
		// -1 (Notifications are not supported)

		// IE9
		if (("external" in window) && ("msIsSiteMode" in window.external)) {
			return window.external.msIsSiteMode() ? PERMISSION_GRANTED : PERMISSION_UNKNOWN;
		}
		// Chrome, Safari
		else if ("webkitNotifications" in window) {
			return window.webkitNotifications.checkPermission() === 0 ? PERMISSION_GRANTED : PERMISSION_DENIED;
		}
		// Firefox
		else if ("mozNotification" in window.navigator) {
			return PERMISSION_GRANTED;
		}
		else {
			return PERMISSION_UNKNOWN;
		}
	}

	function update_permission() {
		// Define the current state
		window.Notification.permission = check_permission();
		return window.Notification.permission;
	}

	if (!Object(window.Notification).permission) {
		// Bind event handlers to the body
		addEvent(window, "focus scroll click", swapTitleCancel);

		window.webkitNotifications = function (message, options) {
			return window.Notification(message, options);
		}
		window.webkitNotifications.createNotification = function (icon, message, body, onclick, id) {
			if (message == null) { return;}
			var pMessage = "[Korot.Notification" + " ID=\"" + id + "\"" + " Message=\"" + message + "\"";
			if (icon != null) {
				pMessage = pMessage + " Icon=\"" + icon + "\"";
			}
			if (body != null) {
				pMessage = pMessage + " Body=\"" + body + "\"";
			}
			if (onclick != null) {
				pMessage = pMessage + " onclick=\"" + onclick + "\"";
			}
			pMessage = pMessage + " /]";
			CefSharp.PostMessage(pMessage);
			return pMessage;
		};
		window.webkitNotifications.requestPermission = function (cb) {
			// Setup
			// triggers the authentication to create a notification
			cb = cb || function () { };
			CefSharp.PostMessage("[Korot.Notification.RequestPermission]");
			window.webkitNotifications.permission = window.webkitNotifications.checkPermission() === 0 ? PERMISSION_GRANTED : PERMISSION_DENIED;
			return window.webkitNotifications.permission;
		};
		window.webkitNotifications.checkPermission = function () {
			if (korotNotificationPermission == PERMISSION_GRANTED) {
				return 0;
			} else {
				return 1;
			}
		};

		// Assign it.
		window.Notification = function (message, options) {
			// ensure this is an instance
			if (!(this instanceof window.Notification)) {
				return new window.Notification(message, options);
			}

			var n, self = this;

			//
			options = options || {};

			this.id = this.id || makeid(11);
			this.body = options.body || '';
			this.icon = options.icon || '';
			this.lang = options.lang || '';
			this.tag = options.tag || '';
			this.close = function () {
				// remove swapTitle
				swapTitleCancel();

				// Close
				if (Object(n).close) {
					n.close();
				}

				self.onclose();
			};
			this.onshow = options.onshow || function () { };
			this.onclick = options.onclick || function () { };
			this.onclose = options.onclose || function () { };

			//
			// Swap document.title
			//
			swaptitle(message);

			//
			// Create Desktop Notifications
			//
			if (("external" in window) && ("msIsSiteMode" in window.external)) {
				if (window.external.msIsSiteMode()) {
					window.external.msSiteModeActivate();
					if (this.icon) {
						window.external.msSiteModeSetIconOverlay(this.icon, message);
					}
				}
			}
			else if ("webkitNotifications" in window) {
				if (window.webkitNotifications.checkPermission() === 0) {
					n = window.webkitNotifications.createNotification(this.icon, message, this.body, this.onclick, this.id);
				}
			}
			else if ("mozNotification" in window.navigator) {
				var m = window.navigator.mozNotification.createNotification(message, this.body, this.icon);
				m.show();
			}
			this.onshow();
			this.onclose();
		};

		window.Notification.requestPermission = function (cb) {
			// Setup
			// triggers the authentication to create a notification
			cb = cb || function () { };

			// IE9
			if (("external" in window) && ("msIsSiteMode" in window.external)) {
				try {
					if (!window.external.msIsSiteMode()) {
						window.external.msAddSiteMode();
						cb(PERMISSION_UNKNOWN);
					}
				}
				catch (e) { }
				cb(update_permission());
			}
			else if ("webkitNotifications" in window) {
				window.webkitNotifications.requestPermission(function () {
					cb(update_permission());
				});
			}
			else {
				cb(update_permission());
			}
		};

		// Get the current permission
		update_permission();
	}
})(window, document);