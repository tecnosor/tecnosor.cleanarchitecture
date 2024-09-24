# The clean architecture framework
A clean architecture framework made by .net Rest Api microservices with the idea to 

## Contains

Following modules are the 

0. DDD Basics
1. Logging utilities
2. Diagnostics utilities
3. Secret Management
4. Events Management
6. Architecture Tests to ensure developer follow the framework rules
7. Match Criteria
8. Pagination
9. Different Extensions


## User guide

### 0. DDD Basics

### 1. Logging


### 2. Diagnostics


### 3. Secret Management


### 4. Events Management



# Appsettings:
Values admited in appsettings of components who integrate this framework/library:

```json
Tecnosor:CleanArchitecture
{ 
	Cuota: {
		Enabled: false | true, // Set cuota limits on api calls (per path) - (false by default)
		MaxLimit: 1..N, // Set cax limit of calls - (1000 by default)
		MinutesInterval: 1..N, // Set interval of the cuota limit (in minutes) - (1 by default)
		QueueLimit: 1..N, // Set limit of the queue - (50 by default)
	},
	Logging: { // TODO - is pending to add external providers like elasticsearch, relational databases, kafka, etc.
		Enabled: false | true, // Indicates if enable logging module (false by default)
		Type: "console" | "file" | "other", // Indicates type of logs (console by default)
		FileConf: { // If Type is file, configuration related to the file
			Path: "your/path", // by default will be `{home}/logs/{apname}`
			Retention: {
				Enabled: false | true, // Indicates if apply rotation - by default will be false
				Unit: "hour" | "daily" | "weekly | "monthly" | "yearly", // Unit of retention - by default monthly
				Value: 1..N, // Value of retention - by default 1 (it will mean 1 month, 4 example)
			}
		},
		EnableQr: false | true, // by default true if environment != PRODUCTION
		EnableAuditRequests: false | true, // by default true
		EnableDebugTraces: false | true // by default true if environment != PRODUCTION
		EnableInfoTraces: false | true // by default true
	},
	JwtAuth: { // TODO - crear
		PrivateCrtPath: "path/to/your/path" // Location of private certificate if needed, used for JWT generation,
		PublicCrtPath: "path/to/your/path"  // Location of private certificate if needed, used for JWT validation
	},
	Diagnostics: {
		
	}
},

```


# Authors

Alfonso Soria Muñoz

# License
<< to define >>

# TODO List:
- end match criteria
- Create nuget
- se pueda definir un decorador [ArchitectureException] para que evite el KO del test
- Define decorator [ArchitectureException] to manage architecture rule exceptions
# Backlog
- separate specific modules into different nugets

