namespace AddMilliseconds_1
{
	using System;
	using Skyline.DataMiner.Analytics.GenericInterface;

	/// <summary>
	/// Represents a data source.
	/// See: https://aka.dataminer.services/gqi-external-data-source for a complete example.
	/// </summary>
	[GQIMetaData(Name = "Add Milliseconds")]
	public sealed class AddMilliseconds : IGQIRowOperator, IGQIInputArguments
    {
        private GQIColumnDropdownArgument _dateColumnArg = new GQIColumnDropdownArgument("Date column") { IsRequired = true, Types = new GQIColumnType[] { GQIColumnType.DateTime } };

        private GQIColumn _dateColumn;

        public GQIArgument[] GetInputArguments()
        {
            return new GQIArgument[] { _dateColumnArg };
        }

        public OnArgumentsProcessedOutputArgs OnArgumentsProcessed(OnArgumentsProcessedInputArgs args)
        {
            _dateColumn = args.GetArgumentValue(_dateColumnArg);

            return new OnArgumentsProcessedOutputArgs();
        }

        public void HandleRow(GQIEditableRow row)
        {
            try
            {
                DateTime dt = row.GetValue<DateTime>(_dateColumn);
                row.SetDisplayValue(_dateColumn, row.GetDisplayValue(_dateColumn) + "." + dt.Millisecond);
            }
            catch (Exception)
            {
                // Catch empty cells
            }
        }
    }
}
