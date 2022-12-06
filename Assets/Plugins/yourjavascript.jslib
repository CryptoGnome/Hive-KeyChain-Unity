mergeInto(LibraryManager.library, {

	Connect: function (username) {
		var user = UTF8ToString(username);
		hive_keychain.requestHandshake(function() {
			console.log("User: " + user);
			//check if user is logged in
			window.hive_keychain.requestSignBuffer(user, 'login', 'Posting', function(response) {
				console.log("Login received!");
				console.log(response);
				if (response['success'] == true) {
					console.log("Login successful, username: " + response['data']['username']);
					SendMessage("Hive", "SetUser", response['data']['username']);
				}
				else {
					console.log("Login failed!");

					return "";
				}
			});
		});
	},


	//TEST CODE
	// requestSignTx
	SignTx: function (username, id, data, prompt) {
		window.hive_keychain.requestCustomJson(UTF8ToString(username), UTF8ToString(id), 'Posting', UTF8ToString(data), UTF8ToString(prompt), function(response) {
			console.log(response);
			if (response['success'] == true) {
				console.log("Transaction successful!");
			}
			else {
				console.log("Transaction failed!");
			}
		});


	},

	///transfer tokens
	Transfer: function (username, to, amount, memo) {
		const id = "ssc-mainnet-hive";
		const json = {
			"contractName": "tokens",
			"contractAction": "transfer",
			"contractPayload": {
				"symbol": "PKM",
				"to": UTF8ToString(to),
				"quantity": "1",
				"memo": UTF8ToString(memo)
			}
	
		};

		//convert json to string
		const data = JSON.stringify(json);
		console.log(data);
		
		//request sign tx
		window.hive_keychain.requestCustomJson(
			UTF8ToString(username), 
			id, 
			'Active', 
			data, 
			'Transfer PKM', 
			function(response) 
			{
			console.log(response);
			if (response['success'] == true) {
				console.log("Transaction successful!");
			}
			else {
				console.log("Transaction failed!");
			}
		});
	}



});

