# SortApi

This is a .NET Core WebAPI which has a POST endpoint which accepts request to sort an integer array. Requests are queued to a background service. Results can be obtained via GET calls.

## Running the application
Two ways to run this application:
1. Open the application in Visual Studio & Run the web project.
2. Alternatively, navigate to SortApi folder & run the below command in the command prompt:
```PowerShell
dotnet run
```
In both the steps, the application runs on port 5000 & below commands can be used to verify the sorting an integer array.

## Sample workflows
1. Intial GET Request:
```PowerShell
curl --request GET "http://localhost:5000/sort"
```
  Empty Response:
```PowerShell
[]
```

2. POST Request to queue a SORT operation:
```PowerShell
curl --request POST --header "Content-Type: application/json" --data "[2,1,3"] "http://localhost:5000/sort"
```
Response:
```PowerShell
{"id":"8b052eab-f11a-49a8-8c44-ec38c0e02605","status":"Pending","duration":null,"input":[2,1,3],"output":null}
```

3. GET Request for previous sort operation:
```PowerShell
curl --request POST --header "Content-Type: application/json" --data "[2,1,3"] "http://localhost:5000/sort"
```

Response:
```PowerShell
{"id":"8b052eab-f11a-49a8-8c44-ec38c0e02605","status":"Completed","duration":"00:00:00.0025450","input":[2,1,3],"output":[1,2,3]}
```

4. Following Step 2, send more sort requests to this API. Using the below curl command(same as step 1), the list of sort operations handled by this API can be viewed:

Request:
```PowerShell
curl --request GET "http://localhost:5000/sort"
```

Response:
```PowerShell
[{"id":"8b052eab-f11a-49a8-8c44-ec38c0e02605","status":"Completed","duration":"00:00:00.0025450","input":[2,1,3],"output":[1,2,3]},{"id":"9ebb9d51-e39d-4edd-9b1e-de988beac56f","status":"Completed","duration":"00:00:00.0001602","input":[100,51,63],"output":[51,63,100]}]
```