using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Employment
    {
        //data members
        private string _Title;
        private double _Years;
        private SupervisoryLevel _Level;
      
        //properties

        //Title cannot be empty
        //since there is validation, the property will be fully-implemented
        //since fully-implemented, one requires a data member
        public string Title
        {
            //referred to as the accessor
            //get is a required item in a property
            get { return _Title; }
            //referred to as the mutator
            //incoming data is accessed using the keyword value
            //the set is an optional item in a property
            //open to the public by default
            //can be restricted to use ONLY with the property
            //  by setting the access to the set to: private
            //IF the set is private the only way to access a value
            //  to the property is via: a constructor OR a behaviour
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Title is required");
                }
                else
                {
                    _Title = value;
                }
            }
        }
        //enum SupervisoryLevel will have additional logic to test if the value
        //      supplied to the field is actually defined in the enum
        //therefore this property will be fully-implemented
        //the private set will NOT allow a piece of code outside of this class
        //  to change the value of the property
        //this will force any code using this class to set the Level either by
        //  the a constructor OR a behaviour
        //it basically makes the Level a read only field when accessed directly
        public SupervisoryLevel Level 
        { 
            get { return _Level; } 
            private set
            {
                //validate that the value given as an enum is actually defined
                //a user of this class could send in an integer value that was
                //  type casted as this enum datatype BUT have a non-defined value
                //to test for a defined enum value use Enum.IsDefined(typeof(xxxx),value)
                //  where xxxx is the name of the enum datatype
                if (!Enum.IsDefined(typeof(SupervisoryLevel), value))
                {
                    throw new ArgumentException($"Supervisory level is invalid: {value}");
                }
                _Level = value;
            }
        }

        //Year will need to be a positive zero or greater value
        //int
        //therefore this property needs to be fully-implemented IF
        //  the validation is within the property
        //NOTE: validations CAN be code elsewhere within your class
        //      if so, you need to restrict the set access to the
        //      property so that any data will be forced through the
        //      validation logic located elsewhere
        public double Years
        {
            get { return _Years; }
            set
            {
                //this validation test is using a method from the static class Utilities
                if (!Utilities.IsZeroPositive(value))
                {
                    throw new ArgumentOutOfRangeException("Year must be a number 0.0 or greater.");

                }
                else
                {
                    _Years = value;
                }
            }
        }

        //this property is an example of an auto-implemented property
        //there is no validation in the property
        //therefore no private data member is required
        //the system will generate an internal storage area for the data
        //      and handle the setting and retreiving from that storage area
        //the private set means that the property will only be able to be
        //      set via a constructor or behaviour
        public DateTime StartDate { get; private set; }

        //constructors
        public Employment()
        {
            //default constructor
            //simulates the "system default constructor"
            Title = "Unknown";
            Level = SupervisoryLevel.TeamMember;
            StartDate = DateTime.Today;
            //optionally one could set years to zero, but that is
            //  the system default for a double, therefore one does
            //  not need to assign a value UNLESS you wish to
        }

        //all optional parameters (ex year) must appear AFTER non-optional parameters
        public Employment(string title, SupervisoryLevel level, 
                            DateTime startdate,double year = 0.0)
        {
            Title=title;
            Level=level;
            Years=year;
            //validation, start date must not exist in the future
            //validation can be done anywhere in your class
            //since the property is auto-implemented AND has a private set,
            //      validation can be done  in the constructor OR a behaviour 
            //      that alters the property
            //IF the validation is done in the property, IT WOULD NOT be an
            //      auto-implemented property BUT a fully-implemented property
            // .Today has a time of 00:00:00 AM
            // .Now has a specific time of day 13:05:45 PM
            //by using the .Today.AddDays(1) you cover all times on a specific date
            if (startdate >= DateTime.Today.AddDays(1))
            {
                throw new ArgumentException($"The start date is in the future thus invalid:" +
                    $"{startdate}");
            }
            StartDate = startdate;
        }


        //behaviours
        public void SetEmploymentResponsibilityLevel(SupervisoryLevel level)
        {
            //the property has a private set
            //therefore this behaviour allows outside code to give the property a value
            Level = level;
        }

        public override string ToString()
        {
            //this string is known as a "comma separate value" string (csv)
            //the get of the property is being used
            return $"{Title},{Level},{StartDate.ToString("MMM dd yyyy")},{Years}";
        }
    }
}
