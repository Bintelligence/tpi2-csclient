# A simple .Net Client for the Turing Pi 2
This is a simple client for the Turing Pi 2 written in C#. It is made in .Net Standard v2.0, so it should work basically
for every kind of .Net version out there that is supported by Microsoft.

The unit tests are written with XUnit which tests all the 'happy flow' responses. 

This client is created based of the documentation of the Turing Pi 2, found [here](https://docs.turingpi.com/docs/turing-pi2-bmc-api).

## Using this client in your own software
I've created a NuGet package, just go to the package manager console and type:

`Install-Package TuringPi2.Client`


