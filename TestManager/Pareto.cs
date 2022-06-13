using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ***REMOVED***GenericTestReports;

namespace TestManager
{
    public partial class Pareto : Form
    {
        // Create empty list of LogFile objects to be filled with data passed from parent form
        private List<LogFile> TestData = new List<LogFile>();

        /// <summary>
        /// Default constructor. Takes data from parent form and calls private methods to build a chart.
        /// </summary>
        /// <param name="TestData"></param>
        public Pareto(object TestData)
        {
            InitializeComponent();

            // Set private field with data passed from parent form
            this.TestData = (List<LogFile>)TestData;

            // Analyze failed test data, calculate statistics on how many of particular test steps failed
            var sortedFailedTestData = getFailedTests(this.TestData);

            UpdateUI(sortedFailedTestData);
        }

        /// <summary>
        /// This method takes List of ParetoData objects and builds diagram from this data.
        /// </summary>
        /// <param name="sortedFailedTestData">List of ParetoData objects being list of most common test failures together with number of occurences.</param>
        private void UpdateUI(List<ParetoData> sortedFailedTestData)
        {
            // Clear chart data
            chart1.Series["Series1"].Points.Clear();

            // Set variable indicating how many top failed steps will be showed in pie chart
            int maxNumberOfFailsShowed = 4;

            // If number of unique names of failed steps is low, adjust maximum number of steps showed on diagram
            if (sortedFailedTestData.Count <= maxNumberOfFailsShowed)
                maxNumberOfFailsShowed = sortedFailedTestData.Count - 1;

            // Set variable indicating how many failed steps are showed 
            int currentNumberOfFailsShowed = 0;

            // Set variable for counting number of occurences of other test steps
            int numberOfOtherStepsFailed = 0;

            // Loop through every object of ParetoData
            foreach (var failedStep in sortedFailedTestData)
            {
                // Check if max number of single test steps showed is exceeded
                if (currentNumberOfFailsShowed <= maxNumberOfFailsShowed)
                {
                    // Create new instance of DataPoint class
                    System.Windows.Forms.DataVisualization.Charting.DataPoint dP = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                    // Set properties of DataPoint object
                    dP.AxisLabel = failedStep.TestStepName;
                    dP.SetValueY(failedStep.Quantity);
                    dP.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
                    dP.IsValueShownAsLabel = true;

                    // Add DataPoint object to chart
                    this.chart1.Series["Series1"].Points.Add(dP);
                }
                else
                {
                    // If op N failures are already added to chart, calculate number of occurences of other steps
                    numberOfOtherStepsFailed += failedStep.Quantity;
                }

                currentNumberOfFailsShowed++;
            }

            if (numberOfOtherStepsFailed > 0)
            {
                // Add other steps to chart
                System.Windows.Forms.DataVisualization.Charting.DataPoint other = new System.Windows.Forms.DataVisualization.Charting.DataPoint();

                other.AxisLabel = "Other";
                other.SetValueY(numberOfOtherStepsFailed);
                other.Font = new Font("Microsoft Sans Serif", 15, FontStyle.Bold);
                other.IsValueShownAsLabel = true;

                this.chart1.Series["Series1"].Points.Add(other);
            }
        }

        /// <summary>
        /// This method creates List of ParetoData objects. Every object handles unique name of failed test and number of its occurences in input data.
        /// </summary>
        /// <param name="testData">List of LogFile objects, handles data harvested from log files.</param>
        /// <returns>List of ParetoData objects.</returns>
        private List<ParetoData> getFailedTests(List<LogFile> testData)
        {
            // Pick up only tests that have 'Failed' status
            var failedTests = testData.Where(status => status.BoardStatus == "Failed" && status.FailedStep != null).ToList();

            // Create new List of strings to handle unique names of failed tests
            var uniqueFailedTests = new List<string>();

            // Loop through all failed test names
            foreach (var test in failedTests)
            {
                // If failed test name is already present on List, continue looping
                if (uniqueFailedTests.Contains(test.FailedStep.Split("\n")[0]))
                    continue;

                // If failed test name is not present on List yet, add it
                uniqueFailedTests.Add(test.FailedStep.Split("\n")[0]);
            }

            // Create new empty list of ParetoData type to be filled with pareto data
            var pareto = new List<ParetoData>();

            // Loop through every unique failed test name
            foreach (var uniqueFailedTest in uniqueFailedTests)
            {
                // Set vaiable indicating number of occurences to 0
                var numberOfOccurences = 0;

                // Loop through every failed test step from data input
                foreach (var test in failedTests)
                {
                    // Check how many times unique failed test name is present in filtered input data
                    if (uniqueFailedTest == test.FailedStep.Split("\n")[0])
                    {
                        numberOfOccurences++;
                    }
                }

                // Add new ParetoData object to list
                pareto.Add(new ParetoData(uniqueFailedTest, numberOfOccurences));
            }
            return pareto.OrderByDescending(x => x.Quantity).ToList();
        }
    }
}
