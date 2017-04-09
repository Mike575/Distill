Aspen Plus V8.4
=================

This file contains information about the Visual Basic .Net modules and Excel 
workbook described in the Aspen Plus Help | Using Aspen Plus | Advanced Features 
| Using the Aspen Plus ActiveX Automation Server 

These files are delivered as Visual Basic .Net basic modules and a Office 2003 
Excel workbook within the Aspen Plus User Interface directory structure in a 
sub-directory called VBDotNetexample.

The following files are available in the VBDotNetexample sub-directory

Filename                 Purpose
--------                 --------------------------------

RunAllExamples.bas       Demonstrates the use of the ActiveX Automation Server from within Demonstrates the use of the Visual Basic modules from within VB.net

VBexamp.xls              Demonstrates the use of the Visual Basic modules from within an Excel workbook

readme.txt               This file

VBAutomationExamples     MS Visual Studio .vbproj and .sln files



The following subroutines are available in RunAllExamples.bas

Subroutine               Purpose
----------               --------------------------------

Main                     Calls all of the other subroutines

OpenSimulation           Opens the connection from Visual Basic to the AspenPlus simulator

CompProf                 Retrieves values for a non-scalar variable with two identifiers

Connectivity             Displays a table showing flowsheet connectivity

GetCollection            Illustrates use of a collection object

GetScalarValues          Retrieves scalar variables from a block

ListBlocks               Retrieves a list of blocks and their attributes

Open                     Opens an existing simulation

ReacCoeff                Retrieves values for a non-scalar variable with three identifiers

Run                      Changes a simulation parameter and re-runs the simulation

TempProf                 Retrieves values for a non-scalar variable with one identifier

UnitsChange              Changes the units of measurement of a variable

UnitsConversion          Retrieves a value both in the display units (psi) and alternative units (atm)

UnitString               Retrieves the units of measurement symbol for a variable

