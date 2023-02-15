Realtime Data Reader

Synopsis:
The Data Reader will periodically read data from a .csv file and populate an STK 
ephemeris file (.e) and STK attitude file (.a).  It uses a named Mutex to synchronize 
with a Data Writer. It uses an .xml file that describes the format of the input data.
The .xml file can also describe T&E Graphs or DataDisplays to populate.

Setup:
Create an .xml file describing the input data.  See Step 3 for details.

Step 1:
Load an STK scenario. The Data Reader will display the first aircraft it finds in 
the Object Name field.  The user can choose another aircraft using ... button or type 
the name of a new aircraft.  The aircraft must use external ephemeris and attitude.  
If a new name is provided, a minimal .e and .a file will be initially created for the aircraft.
If the user wants a T&E Graph or DataDisplay with data from the .csv file, these must exist
by name.

Step 2:
Start the Data Writer.  This process must use a C# Mutex named TE_Mutex.  It must write 
the data to a .csv file.  It must share the file with ReadWrite privilege but does not 
have to close the file after each write.

Step 3:
Make sure the Data location points to where the Data Writer is putting the data.
The user points the Column Data to an .xml file describing the format of the input data.
The column index, starting at zero and mapping must be specified.  The name is optional.
<EphemerisData>
  <ColumnCollection>
    <Column>
      <Name>Time</Name>
      <Mapping>PositionTime</Mapping>
      <Index>0</Index>
    </Column>

The mappings for ephemeris are PositionTime, X, Y, Z.  The Mappings for attitude are Yaw,
Pitch, Roll.  
The user can specify any number of Graphs or DataDisplays or Metrics in the .xml file.  The 
loaded scenario must have these items with matching names.  They will be co-opted to show the 
data requested.  The Graph needs the graph name, column index, name, dimension and multiplier. 
The dimension is the STK dimension for the unit of the data.  The multiplier is the conversion 
from the unit in the .csv file to SI units.
<GraphCollection>
  <Graph>
	<GraphName>Graph</GraphName>
	<Column>
	  <Name>LatRate</Name>
	  <Dimension>AngleRate</Dimension>
	  <Index>6</Index>
	  <Multiplier>0.0174533</Multiplier>
	</Column>
  </Graph>
</GraphCollection>

The DataDisplay needs the display name, column index, name, dimension and unit.  The name
will be what is shown in the DataDisplay.  The unit tells the DataDisplay what STK units to 
show the value in.
<DataDisplayCollection>
  <DataDisplay>
	<DisplayName>DataDisplay</DisplayName>
	<Column>
	  <Name>AltRate</Name>
	  <Dimension>Rate</Dimension>
	  <Unit>m/sec</Unit>
	  <Index>7</Index>
	  <Multiplier>1000</Multiplier>
	</Column>
  </DataDisplay>
</DataDisplayCollection>

The Metric needs the same data as the Graph.  This is useful for getting the data into T&E 
to be used later. 
<MetricCollection>
  <Metric>
    <MetricName>term1</MetricName>
    <Column>
      <Name>term1</Name>
      <Dimension>Distance</Dimension>
      <Index>12</Index>
      <Multiplier>0</Multiplier>
    </Column>
  </Metric>
</MetricCollection>
  
Step 4:
Hit the Start Read button.  This will use the Mutex to gain access to the .csv file.  
It will then read all the data in the file and populate the .e, .a and graph with that 
data.  It maintains a pointer to the last read line.  Then periodically it will re-read 
the file starting where it left off and append the data to the .e, .a and graph. The user 
may stop the timer using the Stop Read button.  The user can restart the periodic reads by 
hitting the Start Read button again.  

Or:
If the user does not want the data to periodically update, he can hit the Manual Refresh 
button. This will read all the data currently in the .csv file and populate the .e, .a and 
graph.  This can be repeated to see the additional data in the file.

Step 5: 
When the Data Writer is finished or the event has been captured, the user should hit Stop 
Read. When the test is finished, the user may want to save the STK scenario to a separate 
location.  The Data Reader creates an STK External Data source to populate the graph.  If 
the input scenario contains this source, it will fail on start of Read.  When the Data 
Reader is closed, it will also bring down STK.

Optional:
At any time during the Read, the user may use the two buttons on the bottom to bring up a 
second  STK with all the data up to that point.  This assumes the user has another STK license
available.  This can be useful to perform a lengthy analysis on the current data while the 
Reader continues populating the input scenario.