window.UniLog = function (arg) {
	console.log ("UNITY", arg);
};

var fbLoginStatus = window.fbLoginStatus = {};
function login(){
	FB.login(function(response) {
      console.log('logged', response);
      window.fbLoginStatus = response;
    }, {scope: 'user_about_me,user_birthday,user_status,user_education_history,user_hometown,user_location,user_status'});
	return false;
};

window.fbAsyncInit = function() {
	FB.init({
		appId      : '200891296772873',
		status     : true, // check login status
		cookie     : true, // enable cookies to allow the server to access the session
		xfbml      : true  // parse XFBML
	});

	FB.Event.subscribe('auth.authResponseChange', function(response) {
		if (response.status == 'connected') {
			document.getElementById('loginButton').style.visibility = 'hidden';
			FB.api('/me', function(response) {console.log(response);});

			window.startUnity();
			window.fbf = [];
			console.log('Get friends');
			window.getFBFriends(function(res){
				window.fbf = res;
				var urls = res.map(function(f){return f.picture.data.url;});
				window.fbf_urls = urls.join(' ');
			});
		} else {
			console.log('whatev..', response);
		}
	});
};

window.getFBFriends = function(cb) {
	FB.api('/me/friends',{fields: 'name,picture', limit: 30} , function(response) {
		console.log('Got friends', response);
		if( !response.error ) {return cb(response.data);}
		else {console.log('ups, some error occurred');}
    });
};

window.startUnity = function() {
	u.initPlugin(jQuery("#unityPlayer")[0], "fbtest.unity3d");
};

(function(d){
var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
if (d.getElementById(id)) {return;}
js = d.createElement('script'); js.id = id; js.async = true;
js.src = "//connect.facebook.net/en_US/all.js";
ref.parentNode.insertBefore(js, ref);
}(document));