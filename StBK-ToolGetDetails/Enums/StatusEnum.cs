using System.ComponentModel;

namespace StBK_ToolGetDetails.Enums
{
    /// <summary>
    /// Detail page status
    /// </summary>
    public enum StatusEnum
    {
        [Description("Company detail page")]
        Company = 1,
        [Description("Detail page of a person")]
        Person = 2,
        [Description("Details page no longer available")]
        Idle = 3
    }
}