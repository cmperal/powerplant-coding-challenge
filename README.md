<h3 align="center">Power Plant Coding Challenge -- Carlos Martin Peral</h3>

  <p align="center">
    Here you will find some instructions to follow when running this solution.
    <br />
  </p>


  <!-- ABOUT THE SOLUTION -->
## About The Solution
This Visual Studio solution (.sln) is the technical assessment provided by Carlos Martin Peral for ENGIE.

The version used to generate it has been Visual Studio 2022

## Solution structure
The solution is comprised of 1 projects as follows:
* "powerplant-coding-challenge"--> It is the project that serves the endpoint which runs over .NET 7.0. 

## Steps to run the application
The provided solution can be run by using either Visual Studio 2022 or as a Docker container.

<h4>Visual Studio</h4>
* Select the configuration option called "https" as follows:

![image](https://github.com/cmperal/powerplant-coding-challenge/assets/22909132/048df846-223c-4715-9e4e-609f162be37c)

* Click over the start button for its compilation and subsequent run (or press F5 instead)

* The application will be raised through the URL https://localhost:8888/swagger/index.html by using Swagger in order to test the endpoint.

![image](https://github.com/cmperal/powerplant-coding-challenge/assets/22909132/4d1adbca-beed-44aa-b117-4abbc1bb0e21)


<h4>Docker container</h4>

* Open powershell (or other terminal if working under other operating systems) and go to the location where the docker file is located. In this solution is in the project folder. 
Type the following commmand to build the image:
```
docker build -t powerplant .
```

* Once the image is available in your Docker engine, run the following command to run the container:
```
docker run -dp 8888:443 powerplant
```


