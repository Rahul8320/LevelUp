using System.Runtime.Serialization;

namespace HPlusSport.API.Constants;

public enum SortBy
{
    [EnumMember(Value = "Id")]
    Id,
    [EnumMember(Value = "Name")]
    Name,
    [EnumMember(Value = "Price")]
    Price,
    [EnumMember(Value = "Sku")]
    Sku,
    [EnumMember(Value = "Category")]
    Category
}

public enum SortOrder
{
    [EnumMember(Value = "Asc")]
    Asc,
    [EnumMember(Value = "Desc")]
    Desc,
}