# End to End Testing using [frisby.js](http://frisbyjs.com)

The testing framework we use for the end to end testing is [frisby.js](http://frisbyjs.com).  

## Install [frisby.js](http://frisbyjs.com)

### Install [npm](https://www.npmjs.com/)

`npm` is the package manager for [node.js](https://nodejs.org/).  
Installing `node.js` will install it.  

### Install [frisby.js](http://frisbyjs.com)

> `npm install -g frisby`

### Install [Jasmine](http://jasmine.github.io/)

`frisby` requires the `Jasmine` test runner.  

> `npm install -g jasmine-node`

## Write Tests

The tests are written using the `frisby` API in a set of files which names end with `spec.js`.  

## Execute the Tests

Go to the directory where these files are located or to the parent directory if these files are located in different folders.  

> `jasmine-node .`

If there are other files ending with `spec.js` in this folder or in one of the sub-folders, then you have to name the files explicitly instead of asking the runner to locate all the files ending with `spec.js` using the `.` shortcut.  

> `jasmine-node test_spec.js` for example

## Resources

1. [frisby.js Documentation](http://frisbyjs.com/docs/api/)
