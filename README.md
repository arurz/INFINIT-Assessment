# GitHub Repository Statistics Service
This project provides a RESTful API to calculate the frequency of letters in JavaScript and TypeScript files from a specified GitHub repository. It uses the GitHub API to fetch repository content and processes the data asynchronously.
## Project Structure
The project follows a three-layer architecture:
- **Controllers**
  Handle incoming HTTP requests, invoke the appropriate services, and return responses.
- **Services**
  Contain the business logic, handle interactions with external APIs, and process data.
- **Data Access Layer (DAL)**
  Stores all models that are required for app.
## Key Components
### **API Controller**
GitHubStatisticsController is the main controller handling requests, such as retrieving letter frequency statistics.

Endpoint:
  GET /api/github/statistics/lettersfrequency: Returns dictionary of letters and their frequencies.
### **Service Layer**
**GitHubStatisticsService:**
- Connects to the GitHub API.
- Retrieves all JavaScript and TypeScript files from the repository.
- Delegates the data to a component for counting letter frequencies.
**LetterFrequencyCounter** is a component that:
- Asynchronously counts the frequency of letters in the given data.
-	Uses ConcurrentDictionary for thread-safe operations.
### **Configuration**
**User Secrets**
To securely store your GitHub token, use User Secrets
```json
{
  "GitHubApiSettings": {
    "GitHubApiUrl": "https://api.github.com/repos",
    "Owner": "lodash",
    "Repository": "lodash",
    "Token": "YourGitHubToken"
  }
}
```
-	GitHubApiUrl - Base URL for the GitHub API.
-	Owner - Repository owner.
-	Repository - Repository name.
-	Token - GitHub personal access token.
  
## Installation and Run
1.	Clone the repository
2.	Navigate to the project directory
3.	Install dependencies
4.	Run the project
   
## Testing
The solution uses xUnit for testing. Tests are located in the INFINIT.Tests project. 
For a more detailed analysis, you can navigate to the TestResults.md file, where you will see the explanations of the application's tests and its results, including benchmarks.
## Key Features
-	Asynchronous Data Processing: Efficiently fetches and processes data from GitHub repositories.
-	Optimized Frequency Counting: Uses ConcurrentDictionary for thread-safe operations.
-	File Retrieval: Automatically retrieves files from nested directories.
-	Configurable Settings: Easily adjustable through User Secrets.
