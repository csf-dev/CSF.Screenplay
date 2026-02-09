# Endpoints
**Endpoints** are fundamental to this extension; they are .NET class instances which describe a piece of API functionality which may be consumed.
The single most important (and obvious) piece of information stored in an endpoint object is the URL (route) which is used to communicate with the API function. 
## Three endpoint classes
The WebAPIs extension offers three classes for which to use in defining the endpoints which you consume. 



, first class concept in this extension. An endpoint object is used to define: 

* The URL (route) at which the endpoint is found 
* Whether use of the endpoint requires any parameters
  * How many parameters
  * The .NET types of the parameter values
  * How those parameters are communicated to the endpoint 
* The endpoint's expected response/result type, if it is expected to return one

Users of this extension *are encouraged to build a library of endpoint objects*, describing the 'surface area' of the API that they wish to exercise.
