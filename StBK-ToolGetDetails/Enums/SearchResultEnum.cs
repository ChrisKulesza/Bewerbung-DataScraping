using System.ComponentModel;

namespace StBK_ToolGetDetails.Enums
{
    public enum SearchResultEnum
    {
        [Description("There is no more detail page for this RID.")]
        DetailsPageNone = 0,
        [Description("The details page of a company was found.")]
        DetailsPageCompany = 1,
        [Description("The details page of a person was found.")]
        DetailsPagePerson = 2
    }
}