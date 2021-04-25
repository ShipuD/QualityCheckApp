
Design:
Used regex for pattern matching
Duplicates: Used linq to find the duplicates
Data reader is base class and used for other providers
implemented is CSV reader-Uses excel library to read( needs excel adapter to be installed if not already installed)
What is done:
1) Run the program that takes full file as argument.
2) The Validation.cs file has all teh vaildations
3) Duplicate is checked first
4) For age:digits check is added ( Nan is checked anyway)
5) signup_date date format check pattern is added(Nan is checked anyway)
6) ConstVaidation conatins all constant strings used
7)Add more validations as part of Dictionary of validation pattern present in Validations.cs

What could be done:
1) Configirable validations so no code changes required.
2) If there is anything that can not be validated using regex pattern , we could extend the Valdation using interfaces
2) Add Unit test case
3) Make it more easy to add 