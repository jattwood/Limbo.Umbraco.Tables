﻿using System.Text.Json.Serialization;
using Limbo.Umbraco.Tables.Json.Microsoft.Converters;
using Limbo.Umbraco.Tables.Parsers;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Newtonsoft.Converters;
using Skybrud.Essentials.Json.Newtonsoft.Extensions;

namespace Limbo.Umbraco.Tables.Models;

/// <summary>
/// Class representing a cell in a <see cref="TableModel"/> value.
/// </summary>
public class TableCell : TableObject {

    /// <summary>
    /// Gets the row index.
    /// </summary>
    [JsonProperty("rowIndex")]
    [JsonPropertyName("rowIndex")]
    public int RowIndex { get; }

    /// <summary>
    /// Gets a reference to the row.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public TableRow Row { get; }

    /// <summary>
    /// Gets the column index.
    /// </summary>
    [JsonProperty("columnIndex")]
    [JsonPropertyName("columnIndex")]
    public int ColumnIndex { get; }

    /// <summary>
    /// Gets a reference to the column.
    /// </summary>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public TableColumn Column { get; }

    /// <summary>
    /// Gets a reference to the column value.
    /// </summary>
    [JsonProperty("value")]
    [JsonPropertyName("value")]
    [Newtonsoft.Json.JsonConverter(typeof(StringJsonConverter))]
    [System.Text.Json.Serialization.JsonConverter(typeof(HtmlContentJsonConverter))]
    public IHtmlContent Value { get; }

    /// <summary>
    /// Gets a reference to the type of the cell - eg. <see cref="TableCellType.Td"/> or <see cref="TableCellType.Th"/>.
    /// </summary>
    [JsonProperty("type")]
    [JsonPropertyName("type")]
    public TableCellType Type { get; }

    /// <summary>
    /// Gets a reference to the scope of the cell - eg. <c>row</c> or <c>col</c>.
    /// </summary>
    [JsonProperty("scope")]
    [JsonPropertyName("scope")]
    public TableCellScope Scope { get; }

    internal TableCell(JObject json, int rowIndex, TableRow row, int columnIndex, TableColumn column, TablesHtmlParser htmlParser, bool preview) : base(json) {
        RowIndex = rowIndex;
        Row = row;
        ColumnIndex = columnIndex;
        Column = column;
        Value = new HtmlString(json.GetString("value", x => htmlParser.Parse(x, preview))!);
        Type = row.IsHeader || column.IsHeader ? TableCellType.Th : TableCellType.Td;
        Scope = json.GetEnum("scope", TableCellScope.None);
    }

}