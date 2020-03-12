# Title - Sample ASP.NET MVC CORE CI/CD Pipeline using Azure DevOps. 

# Overview

This repository contains the sample source code to setup Asp.Net Core with Sql Server as Database pipeline 
to deploy resources on Microsoft Azure Public Cloud using ARM Templates or Terraform 

## Technology Stack

1. C#
1. Microsoft .Net Core 2.0
1. ASP.NET MVC Core
1. SQL Server
1. Visual Studio 2017/2019

## Azure Services

1. Azure Resource Manager
1. Azure App Service
1. Azure Storage Account
1. Azure SQL Server

## DevOps Tools

1. CICD - Azure DevOps
1. Static Code Analysis - SonarCloud and Buildbreaker
1. Security Scan - WhiteSource Bolt
1. Smoke Testing - Selenium UI
1. Unit Test - Microsoft Visual Studio XUnit tests
1. Code Coverage - OpenCover, ReportGenerator and CoberturaParser
1. Performance and Load Tests - JMeter
1. Source Repository - GitHub
1. Artifact - Azure Pipeline Artifact
1. Build Agent - Microsoft Hosted Agents
1. Infrastructure Provisioning - ARM Templates and Terraform

# Status Badge

|   | Status |
|---|:-----:|
| SonarCloud Quality Gate | [![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=csharpdocs-aspnet-mvc-core-employee)](https://sonarcloud.io/dashboard?id=csharpdocs-aspnet-mvc-core-employee)
| Azure Pipelines |

# Get Started

Step by step approach to setup CICD Pipeline for ASP.NET MVC Core Application with SQL Server as Database

## Pre-requisite

### 1. Accounts needed

1. Azure DevOps Account([Click here](https://) to create free account) for CICD Orchestration
2. Azure Subscription([Click here](https://) to create free account)
3. Source repository options in Azure Repos, GitHub, GitLab so that you can use SonarCloud. I would recommend to use GitHub
4. SonarCloud Account([Click here](https://sonarcloud.io/) to create free account)

### 2. Install Azure DevOps Extensions

Install below Azure DevOps extension from marketplace

[SonarCloud](https://marketplace.visualstudio.com/items?itemName=SonarSource.sonarcloud)
[SonarCloud build breaker](https://marketplace.visualstudio.com/items?itemName=SimondeLang.sonarcloud-buildbreaker)
[ReportGenerator](https://marketplace.visualstudio.com/items?itemName=Palmmedia.reportgenerator)
[WhiteSource Bolt](https://marketplace.visualstudio.com/items?itemName=whitesource.ws-bolt)
[Terraform](https://marketplace.visualstudio.com/items?itemName=ms-devlabs.custom-terraform-tasks)

## Step by step approach

### 1. Clone source repository

There are multiple ways to clone repository

1. Import GitHub repository in Azure DevOps Repos. Clone it locally and compile application.  

2. Create Azure DevOps Service Connection for GitHub(fork my repository in your GitHub account :)) and use that service connection in Pipelines.
   Follow below path to setup pipeline later

   `Azure DevOps => Pipelines => Builds => New build pipeline => Connect Tab(GitHub [Yaml]) => Select Tab[Select GitHub repository] =>
   GitHub[Approve & Install Azure Pipelines] => Configure Tab [Starter pipeline and select azure-pipelines-ci from solution\definitions\build folder 
   => Save and Run =>  Verify that the pipeline is executed successfully.`

   In case of any difficulties rafer [Microsoft Documentation](https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/template-tutorial-use-azure-pipelines#create-a-pipeline)

Please find below clone Url.

` git clone https://github.com/csharpdocs/aspnet-mvc-core-employee-basic.git `

### 2. Build Solution

1. Open `EmployeeWithDatabaseAzureDevOps.sol` file with Visual Studio 2017 and build the entire solution and fix the 
build error if any. Visual Studio 2019 dosent support .Net Core < 3.0 hence it is advised to upgrade the .Net Core version 
to all projects within solution and also modify the build.yml file based on .Net core target version.

2. Please make sure the required sunsetting files present. This solution already included but still verify again.  

### 3. Add runsetting in test projects

#### 3.1 Selenium Project

Embed below source to your .runsetting file at root level of selenium project 

```xml
	<?xml version="1.0" encoding="utf-8" ?>
	<!--Docs: https://docs.microsoft.com/fr-ca/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2017-->
	<RunSettings>
	  <!-- Parameters used by tests at runtime -->
	  <TestRunParameters>
		<Parameter name="webAppUrl" value="<your azure app service website name>" />
	  </TestRunParameters>
	</RunSettings>
```

#### 3.2. Unit Test Project

Embed below source to your `coverlet.runsetting` file at root level of each unit test project. I used Xunit for unit testing

```xml
	<?xml version="1.0" encoding="utf-8" ?>
	<RunSettings>
	  <DataCollectionRunSettings>
		<DataCollectors>
		  <DataCollector friendlyName="XPlat code coverage">
			<Configuration>
			  <Format>opencover</Format>
			</Configuration>
		  </DataCollector>
		</DataCollectors>
	  </DataCollectionRunSettings>
	</RunSettings>
```
#### 3.3. NuGet Package Installation

Install `coverlet.console`, `coverlet.collector` and `coverlet.msbuild` package in each test project

### 4. Create variable group(Practice.Generic.VariableGroup in this pipeline) and add below keys in it in order to use SonarCloud
Changes value for each key according to your SonarCloud account

1. Sonar.Organization
1. Sonar.ProjectKey
1. Sonar.ProjectName
1. Sonar.ProjectVersion

### 5. Setup your CI Pipeline

In order to setup your pipelie you can us ci/cd orchestration tools like Azure DevOps, Jenkins, GitLab and others too. 
Here I have used my favorite Azure DevOps as ci/cd orchestration tool. 

#### 5.1 User GitHub as a reference repository

Create Azure DevOps Service Connection for GitHub and follow below path

`Azure DevOps => Pipelines => Builds => New build pipeline => Connect Tab(GitHub [Yaml]) => Select Tab[Select GitHub repository] =>
GitHub[Approve & Install Azure Pipelines] => Configure Tab [Starter pipeline and select azure-pipelines-ci from solution\definitions\build folder 
=> Save and Run =>  Verify that the pipeline is executed successfully.`

#### 5.1 User Azure Repos as a reference repository

1. Download my source code as Zip, extract it.
1. Create Azure Repository and push downloaded source in it.
1. To setup pipeline follow below path

` Azure DevOps => Pipelines => Builds => New build pipeline => Connect Tab(Azure Repos Git [Yaml]) => Select repository
=> Configure Tab(Select Existing Azure Pipelines YAML file from available options) => Add azure-pipeline-ci.yml path(solution/definitions/build/azure-pipelines-ci.yml) and click continue button
=> Run =>  Verify that the pipeline is executed successfully. `

### 6. Verify results on SonarCloud

# Relavant Articles

[Azure DevOps Labs](https://www.azuredevopslabs.com/labs/vstsextend/sonarcloud/)
[Microsoft Learn](https://docs.microsoft.com/en-us/learn/modules/scan-for-vulnerabilities/5-scan-pipeline)

# Feedback

Please contact me via comments section or follow/connect me on below social networks

[Csharpdocs.com](http://www.csharpdocs.com)
[LinkedIn](https://www.linkedin.com/in/rohitnadhe) 
[Facebook](https://www.facebook.com/csharpdocs) 
[Twitter](https://twitter.com/csharpdocs) 