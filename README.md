ProwlSimplSharp
===============

A Prowl client for Crestron 3-Series Processors

Note: This only runs on Crestron Hardware.


## Requirements

* Prowl Account [http://prowlapp.com](http://prowlapp.com)
* iOS device with the Prowl App ($2.99)
* 3-Series Processor (v1.007.0014+)


## Usage:

```
ProwlClient client = new ProwlClient();

// Add a key first, you may add as many as you like
// It returns 0, if your key is invalid
int status = client.AddKey("YOUR_API_KEY");


// Send a message to Prowl
// You'll get -1 if you haven't added any keys
int http_status = client.Send("Crestron", priority, "Crestron://", "Subject", "Messsage");

```
