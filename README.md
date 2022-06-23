# XMART
	eCommerce system. 

# About:
      Implementation of an eCommerce System included in the Work Shop course
      including UnitTests, IntegrationTests and ConcurrencyTests.
      Fully implemntation of a ServiceLayer and DomainLayer(Business),Client and a Server.

# Initiation Steps
	There are two steps to initiate the system.
	1) Start up the ServerApi.
	2) Start up the eCommerceClient.

# Configuration
	Using the config file we can custom configure the system for instance changing the default
	admin credentials , external system API URL.
	
	File Format:
	{
		"dbenv": <data base collection>,
		"email": <Admin Username/Email>,
		"password": <Admin Password>,
		"mongoDB_url": <Database URL>,
		"externalSystem_url": <External System URL>
	}

# State File Initiation
	You have the ability to write scenarios where you can test the system.
	You Provide the eCommerceClient with a path to a Initiation File ,
	and it runs the functions inside of it.
	
	For Example:
		Our State Initiation File (Watch out for the format).
		
		--- Register Users
			[{"Method":"Register","Params":["Eran","passEran"]},
			{"Method":"Register","Params":["Masalha","passMasalha"]},
			{"Method":"Register","Params":["Tareq","passTareq"]},
			{"Method":"Register","Params":["Yazan","passYazan"]},
			{"Method":"Register","Params":["Orabi","passOrabi"]},
			{"Method":"Register","Params":["Rami","passRami"]},
			{"Method":"Login","Params":["Eran","passEran"]},
		--- Login Users
			{"Method":"Login","Params":["Rami","passRami"]},
			{"Method":"Login","Params":["Orabi","passOrabi"]},
			{"Method":"Login","Params":["Tareq","passTareq"]},
		--- Open Store \ Store Func
			{"Method":"OpenNewStoreUserName","Params":["Eran's store","Eran"]},
			{"Method":"AddStoreManagerInitFile","Params":["Masalha","Eran","Eran's store"]},
			{"Method":"AddProductToStoreInitFile","Params":["Eran","Eran's store","Bamba","3","100","Snacks",null]},
			{"Method":"AddProductToStoreInitFile","Params":["Eran","Eran's store","FunkoPop","40","10","Anime",null]},
			{"Method":"AddProductToStoreInitFile","Params":["Eran","Eran's store","BlackHoodie","250","25","Attire",null]},
			{"Method":"OpenNewStoreUserName","Params":["Rami's store","Rami"]},
			{"Method":"AddStoreManagerInitFile","Params":["Yazan","Rami","Rami's store"]},
			{"Method":"AddStoreOwnerInitFile","Params":["Tareq","Rami","Rami's store"]},
			{"Method":"AddProductToStoreInitFile","Params":["Rami","Rami's store","Razer Mouse","150","20","PCGadjets",null]},
			{"Method":"AddProductToStoreInitFile","Params":["Rami","Rami's store","Logitech Mouse","175","15","PCGadjets",null]},
			{"Method":"AddProductToStoreInitFile","Params":["Rami","Rami's store","Samsung fridge lol","1500","5","Electronics",null]},
			{"Method":"AddProductToCartInitFile","Params":["Orabi","Bamba","5","Eran's store"]},
			{"Method":"AddProductToCartInitFile","Params":["Orabi","FunkoPop","2","Eran's store"]},
			{"Method":"AddProductToCartInitFile","Params":["Tareq","Razer Mouse","1","Rami's store"]},
			{"Method":"AddProductToCartInitFile","Params":["Rami","BlackHoodie","2","Eran's store"]},
			{"Method":"AddProductToCartInitFile","Params":["Eran","Samsung fridge lol","1","Rami's store"]},
		--- Loging out Active User after Initiation
			{"Method":"LogOutInitFile","Params":["Tareq"]},
			{"Method":"LogOutInitFile","Params":["Eran"]},
			{"Method":"LogOutInitFile","Params":["Rami"]},
			{"Method":"LogOutInitFile","Params":["Orabi"]}]
