# User API Dev Guide
## Building
The project should be buildable from either the dotnet CLI, my preference is from within visual studio and you can build > run from there without any issues on localhost. The project will automatically take you to the swagger index page as there are no views to display.

## Testing
### Unit Tests
Can be run from inside visual studio using the test explorer. These tests will test the business and data layers.

### Integration Tests
I've run out of time and haven't created a proper suite of postman tests, but running the project will start the website and then you can navigate to https://localhost:44350/swagger/index.html and use the generated swagger page to test against the project. It means there isn't a fully automated set of tests but that would be a good phase 2 before adding in new functionality.

## Deploying
Unfortunately haven't had time to acquaint myself with Docker, I spent that time configuring EF mocking in the data layer instead. I've cleared out the SQL server and postgres database service configurations from the docker compose file though, so it might work for you.

## Additional Information
I haven't included any testing for the controllers as I've moved all the logic from them to the business layer. I was looking at creating HTTPClients in the test project and making sure the correct responses were coming back, but as that's mostly for integration testing I want to make sure that client developers could test using swagger, and that'll give them back the correct response types and data, as well as any error messages.

This might be the wrong attitude but I don't want to create a suite of automated regression tests inside the project, as I want consumers of the API to be able to execute regression tests so they can confirm everything works as the technical specification required before they start building their code. That's a personal choice based off wanting different client applications to be able to know that the API is functioning so they can tell at a glance if any bugs are with the API or their application.