Content Foundation (CF)
===

### What is content foundation
Content Foundation is a Content Management Framework (CMF) that lets users manage all variety of information from a website, using a web browser on a smartphone, tablet, or desktop computer. Typically, the CMF software runs on a computer where a database and a web server are installed. The content and settings for the website are usually stored in a database, and for each page request that comes to the web server, the scripts combine information from the database and assets (JavaScript files, CSS files, image files, etc. that are part of the CMF or have been uploaded) to build the pages of the website.  

### What does content foundation do
Based on CF, developers could build up a new functional Information System without too much tedious work. In the other words, CF is the architecture of CMS (Content Management System). 

### Key components of content foundation
* [Custom Entity Foundation (entity storage)](https://github.com/Haiping-Chen/CustomEntityFoundation)
* [Quickflow (workflow engine)](https://github.com/Haiping-Chen/Quickflow)
* Data Presenter (data visualization)
* User Web Interface (UI)

![Image](https://raw.githubusercontent.com/Haiping-Chen/ContentFoundation/master/Content%20Foundation%20Components.png)

### How to install
#### Install CF Core
````sh
Install-Package ContentFoundation.Core
````
#### Install CF Restfual Api
````sh
Install-Package ContentFoundation.RestApi
````
#### Install CF UI
````sh
npm install cf-ui
````
