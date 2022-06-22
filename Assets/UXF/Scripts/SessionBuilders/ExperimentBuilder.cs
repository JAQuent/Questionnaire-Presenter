using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace UXF
{
    public class ExperimentBuilder : MonoBehaviour, IExperimentBuilder
    {

        [Tooltip("The name key in the settings that contains the name of the trial specification file.")]
        [SerializeField] private string fileKey = "trial_specification_name";
        [Tooltip("Character to for separating the values")]
        [SerializeField] private string separator = ",";
        [Tooltip("Enable to copy all settings from each trial in the TSV file to the the trial results output.")]
        [SerializeField] private bool copyToResults = true;

        /// <summary>
        /// Reads a TSV from filepath as specified in fileKey in the settings.
        /// The TSV file is used to generate trials row-by-row, assigning a setting per column.
        /// </summary>
        /// <param name="session"></param>
        public void BuildExperiment(Session session)
        {
            // check if settings contains the tsv file name
            if (!session.settings.ContainsKey(fileKey))
            {
                throw new Exception($"File name not specified in settings. Please specify a file name in the settings with key \"{fileKey}\".");
            }

            // get the tsv file name
            string fileName = session.settings.GetString(fileKey);

            // check if the file exists
            string filePath = Path.GetFullPath(Path.Combine(Application.streamingAssetsPath, fileName));
            if (!File.Exists(filePath))
            {
                throw new Exception($"File at \"{filePath}\" does not exist!");
            }

            // read the tsv file
            string[] lines = File.ReadAllLines(filePath);

            // Convert string to char
            separator = separator.Replace(@"\t", "\t");
            char separator_char = char.Parse(separator);

            // parse as table
            var table = UXFDataTable.FromLINES(lines, separator_char);

            // build the experiment.
            // this adds a new trial to the session for each row in the table
            // the trial will be created with the settings from the values from the table
            // if "block_num" is specified in the table, the trial will be added to the block with that number
            session.BuildFromTable(table, copyToResults);
        }
    }

}
