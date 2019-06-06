using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp3
{
    /// <summary>
    /// The class Aggregate is the class that will contain the functions that can be used
    /// to process data files and eventually print the test results into an output file
    /// </summary>
    public class Aggregate
    {
        /// <summary>
        /// Three dictionaries are declared to store the aggregate values.
        /// Each of the dictionary stores the pairs of ( Name, Number ) where the 
        /// "Number" is the total number of brown eyed people associated with 
        /// the "Name" of respective country, city and gender respectively.
        /// The reason UInt64 has been chosen is because the numbers are always non-negative
        /// and 64 bits long integer is more suitable for storing large numbers.
        /// </summary>
        private Dictionary<string, UInt64> country;
        private Dictionary<string, UInt64> city;
        private Dictionary<string, UInt64> gender;

        private const string defaultinputPath = "C:\\Users\\Public\\inputTest.txt";
        private const string defaultoutputPath = "C:\\Users\\Public\\outputFile.txt";

        /// <summary>
        /// The constructor method for the class where memory is being
        /// allocated for all three dictionary objects
        /// </summary>
        public Aggregate()
        {
            country = new Dictionary<string, UInt64>();
            city = new Dictionary<string, UInt64>();
            gender = new Dictionary<string, UInt64>();
        }

        /// <summary>
        /// The function updates the three dictionaries based on the data passed in
        /// </summary>
        /// <param name="splitContent"> splitContent[0], splitContent[1] and splitContent[2] 
        /// are the names of country, city and gender respectively </param>
        /// <param name="x"></param>
        private void UpdateDictionaries( string[] splitContent, UInt64 x )
        {
            if ( country.ContainsKey( splitContent[0] ) )
            {
                /// If it is a known country, we add the number x to existing value
                country[splitContent[0]] += x;
            }
            else
            {
                /// If it is a new country (not seen so far), add the enty to the dictionary
                country[splitContent[0]] = x;
            }

            if ( city.ContainsKey( splitContent[1] ) )
            {
                // If it is a city known to have certain number of brown-eyed, we add the number x to its value
                city[splitContent[1]] += x;
            }
            else
            {
                // If it is a new city (not seen so far), add the enty to the dictionary
                city[splitContent[1]] = x;
            }

            if ( gender.ContainsKey( splitContent[2] ) )
            { 
                /// we increment the respective gender number by an amount of x
                gender[splitContent[2]] += x;
            }
            else
            {
                /// We add the entry ( gender, number) since a data line with the gender is processed first time
                gender[splitContent[2]] = x;
            }
        }

        /// <summary>
        /// The function processes a string "content" which is a single line of data in the input file.
        /// It first tries to split the "content" into four parts separated by tab character. 
        /// If the string "content" could not be split into four parts based on tab character, 
        /// the function will not process the string any further and return "false".
        /// Even if it cannot parse the final string into an unsigned int, it will return "false"
        /// </summary>
        /// <param name="content"> It is a single line of data in the input file</param>
        /// <returns>The return value is "true" if the string content is parsable into four strings
        /// and the last string is a non-negative integer and "false" otherwise. </returns>
        public Boolean processDataLine( string content )
        {
            UInt64 x = 0;
            bool res = true;
            string sep = "\t";

            /// Splitting the "content" passed into this function 
            string[] splitContent = content.Split( sep.ToCharArray() );
            if ( splitContent.Length != 4 )
            {
                // Providing Diagnostic data to the user with regard to input data
                System.Console.WriteLine( "The contents of the line are not well aligned" );
                System.Console.WriteLine( "Ensure that the strings in the input line are separated by tabs" );
                System.Console.WriteLine( "Skipping the line from further processing" );
                res = false;
            }
            else
            {
                /// We use TryParse to ensure that the last string splitContent[3] is indeed 
                /// a non-negative integer and can be used to update the dictionaries
                res = UInt64.TryParse( splitContent[3], out x );
                if ( res )
                {
                    /// Since we have a proper x, we will update the dictionaries.
                    UpdateDictionaries( splitContent, x );
                }
                else
                {
                    /// We log the error to the Console when we cannot parse the last string as an integer
                    System.Console.WriteLine( "The string {0} cannot be parsed into a non-negative integer", splitContent[3] );
                    System.Console.WriteLine( "Skipping the line from further processing" );
                }
            }
            return res;
        }

        /// <summary>
        /// This function processes the data file given as input (i.e. the argument inputFile).
        /// It will call processDataLine() for every line that it successfully reads from
        /// the input file
        /// </summary>
        /// <param name="inputFile"> The path of the input file </param>
        /// <returns> This will return true if the entire file is processed and all lines in the
        /// input data are formatted correctly and have been processed without an issue. 
        /// It will return false otherwise. </returns>
        public Boolean processDataFile( string inputFile = defaultinputPath )
        {
            Boolean res = true;
            string content;

            try
            {
                // Try to open the input file so that the data can be read and processed.  
                System.IO.StreamReader file =
                    new System.IO.StreamReader( inputFile );
                if ( file != null )
                {
                    System.Console.WriteLine("Input File {0} opened Successfully", inputFile );
                }
                else
                {
                    System.Console.WriteLine( "Input File {0} could not be opened", inputFile );
                }
                  
                while ( true )
                {
                    /// The data is read one line at a time.
                    /// Any exception during the ReadLine function will be caught
                    /// and reported, i.e. logged to the console in the code below.
                    content = file.ReadLine();
                    if ( content != null )
                    {
                        System.Console.WriteLine( "Successfully read the line:\n{0}", content );
                    }
                    else
                    {
                        /// We break out of the loop when we are done with reading the lines
                        break;
                    }
                    
                    /// Even if one line cannot be processed, we will let the Boolean "res" to be false.
                    /// This way, we get to know that every line of the input file has been processed without an issue 
                    
                    res = ( res && processDataLine( content ) );
                    
                }
                
                file.Close();
                System.Console.WriteLine( "The input file {0} is closed", inputFile );
            }
            catch ( Exception e )
            {
                // Printing the exception message
                System.Console.WriteLine( "Exception: " + e.Message );
                System.Console.WriteLine( "Cannot read the file {0} anymore due to exception above", inputFile );
            }
            
            return res;
        }

        /// <summary>
        /// The function is used to print the final results into output file.
        /// </summary>
        /// <param name="outputfile"> This is the full path of the file into which the output is printed into. 
        /// If no path is provided, the defaultoutputpath is made use of. </param>
        public void printResults( string outputfile = defaultoutputPath )
        {
            try
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter( outputfile );

                System.Console.WriteLine( "Writing to the output file {0}", outputfile );

                foreach ( var item in country )
                {
                    file.WriteLine("{0}\t{1}", item.Key, item.Value);
                }
                foreach ( var item in city )
                {
                    file.WriteLine("{0}\t{1}", item.Key, item.Value);
                }
                foreach ( var item in gender )
                {
                    file.WriteLine("{0}\t{1}", item.Key, item.Value);
                }

                file.Close();

                System.Console.WriteLine( "Writing to the output file complete" );
            }
            catch( Exception e )
            {
                // Logging to the console that an exception has occured
                System.Console.WriteLine( "Exception: " + e.Message );
                System.Console.WriteLine( "The output could not be written to {0}", outputfile );
            }

        }
    }


    class Program
    {
    /// <summary>
    /// The main function is used as a test driver for the class Aggregate
    /// The functions processDataFile and printResults are called to test it. 
    /// </summary>
    /// <param name="args"></param>
        static void Main( string[] args )
        {

            Aggregate root = new Aggregate();

            Boolean b = root.processDataFile();

            root.printResults();

           System.Console.ReadKey();

        }
    }
}