var frisby = require('frisby');

/*
Use the following for debug
	.inspectJSON()      // Prints response json to stdout
	.inspectHeaders()   // Prints out all the headers in response
*/

var rootURL = 'http://localhost:49602/api/donors';

var name = "Aowss";
var email = "aowss@yahoo.com";
var postcode = "SE193HQ";
var phonenumber = '0044777888999';

var donor = {
   "NAME": name,
   "TYPE": 0,
   "ADD1": "test",
   "ADD2": "",
   "CITY": "Amsterdam",
   "PCODE": postcode,
   "EMAIL": email,
   "COUNTY": "",
   "COUNTRY": "United Kingdom",
   "TEL": phonenumber,
   "TEL_WORK": null,
   "MOBILE": "",
   "NTUSERWHOADDED": null,
   "TITLE": null,
   "ERECEIPT": true,
   "GAD": true
}

var wrongName = "Wrong-Aowss";
var wrongEmail = "Wrong-aowss@yahoo.com";
var wrongPostcode = "Wrong-SE193HQ"

frisby.create('Add Donor')
	.post( rootURL, donor, {json: true} )
	.expectStatus(201)
	.expectHeaderContains('Location', rootURL)
	.expectHeader('Content-Length', '0')
	.toss();

frisby.create('Add Donor and retrieve using Location header')
	.post( rootURL, donor, {json: true} )
	.after(function(err, res, body) {
		var location = res.headers['location'];	//	.NET Web API 2 seems to use lower case for the headers
		frisby.create('Get the new Donor')
			.get(location, {
				headers:  {
					"Accept": "application/json"
				}
			})
			.expectStatus(200)
			.expectHeaderContains('Content-Type', 'application/json')	// use expectHeaderContains instead of expectHeader to cater for 'application/json; charset=utf-8'
			.expectJSON({
				NAME: name,
				PCODE: postcode,
				EMAIL: email
			})
			.toss()
	})
	.toss();

frisby.create('Search Donors by Code')
	.post( rootURL, donor, {json: true} )
	.after(function(err, res, body) {
		var code = res.headers['location'].split("/").pop();
		frisby.create('Search by Code')
			.get(rootURL + '?Code=' + code, {
				headers:  {
					"Accept": "application/json"
				}
			})
			.expectStatus(200)
			.expectJSON({
				NAME: name,
				PCODE: postcode,
				EMAIL: email
			})
			.toss()
	})
	.toss();

frisby.create('Search Donors by Email')
	.post( rootURL, donor, {json: true} )
	.after(function(err, res, body) {
		frisby.create('Search by Email')
			.get(rootURL + '?Email=' + email, {
				headers:  {
					"Accept": "application/json"
				}
			})
			.expectStatus(200)
			//	TODO: check that the JSON response contains an array of at least one element
			.expectJSON('*', {
				EMAIL: email
			})
			.toss()
	})
	.toss();

frisby.create('Search Donors by Postcode')
	.post( rootURL, donor, {json: true} )
	.after(function(err, res, body) {
		frisby.create('Search by Postcode')
			.get(rootURL + '?PostCode=' + postcode, {
				headers:  {
					"Accept": "application/json"
				}
			})
			.expectStatus(200)
			//	TODO: check that the JSON response contains an array of at least one element
			.expectJSON('*', {
				PCODE: postcode
			})
			.toss()
	})
	.toss();

frisby.create('Search Donors by Phone')
	.post( rootURL, donor, {json: true} )
	.after(function(err, res, body) {
		frisby.create('Search by Phone')
			.get(rootURL + '?PhoneNumber=' + phonenumber, {
				headers:  {
					"Accept": "application/json"
				}
			})
			.expectStatus(200)
			//	TODO: check that the JSON response contains an array of at least one element
			.expectJSON('*', {
				TEL: phonenumber
			})
			.toss()
	})
	.toss();

frisby.create('Update Donor')
	.post( rootURL, donor, {json: true} )
	.after(function(err, res, body) {
		var location = res.headers['location'];	//	.NET Web API 2 seems to use lower case for the headers
		frisby.create('Get the Donor before Update')
			.get(location, {
				headers:  {
					"Accept": "application/json"
				}
			})
			.expectStatus(200)
			.expectJSON({
				NAME: name
			})
			.after(function(err, res, body) {
				var updatedDonor = donor;
				updatedDonor.NAME = wrongName;
				//console.log("updated donor : " + JSON.stringify(updatedDonor));
				frisby.create('Update the Donor')
					.put(location, updatedDonor, {json: true} )
					.expectStatus(204)	//	PUT returns 204
					.after(function(err, res, body) {
						frisby.create('Get the updated Donor')
							.get(location, {
								headers:  {
									"Accept": "application/json"
								}
							})
							.expectStatus(200)
							//.inspectJSON()
							.expectJSON({
								NAME: wrongName
							})
							.toss()
					})
					.toss()
			})
			.toss()
	})
	.toss();

frisby.create('Delete Donor')
	.post( rootURL, donor, {json: true} )
	.after(function(err, res, body) {
		var location = res.headers['location'];	//	.NET Web API 2 seems to use lower case for the headers
		frisby.create('Get the Donor before Delete')
			.get(location, {
				headers:  {
					"Accept": "application/json"
				}
			})
			.expectStatus(200)
			.expectJSON({
				NAME: name
			})
			.after(function(err, res, body) {
				frisby.create('Delete the Donor')
					.delete(location)
					.expectStatus(204)
					.after(function(err, res, body) {
						frisby.create('Try to Get the deleted Donor')
							.get(location, {
								headers:  {
									"Accept": "application/json"
								}
							})
							.expectStatus(404)
							.toss()
					})
					.toss()
			})
			.toss()
	})
	.toss();

frisby.create('Delete an inexistant Donor')
	.delete(rootURL + '/1000000')
	.expectStatus(404)
	.toss();

	
frisby.create('Update an inexistant Donor')
	.put( rootURL + '/1000000', donor, {json: true} )
	.expectStatus(404)
	.toss();

frisby.create('Update a Donor passing no data')
	.put( rootURL + '/1000000', '', {json: true} )
	.expectStatus(400)
	.toss();
