using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Unicode;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Vivelin.Web.Home.Pages.Tools;

public class InspectorModel : PageModel
{
    public void OnGet()
    {
        if (HasText)
        {
            CodePoints = Text.EnumerateRunes()
                .Select(x => new CodePoint(x))
                .Where(x => !Skip || x.IsInteresting)
                .ToImmutableList();
        }
    }

    [FromQuery]
    public string? Text { get; set; }

    [FromQuery]
    public bool Skip { get; set; }

    [MemberNotNullWhen(true, nameof(Text))]
    public bool HasText => Text != null;

    public IReadOnlyList<CodePoint> CodePoints { get; set; }
        = new List<CodePoint>();

    public record CodePoint
    {
        public CodePoint(Rune rune)
        {
            var charInfo = UnicodeInfo.GetCharInfo(rune.Value);
            DisplayText = charInfo.GetDisplayText();
            Name = charInfo.Name ?? charInfo.OldName;
            CodePointValue = $"U+{rune.Value:X4}";
            Category = charInfo.Category;
            Block = charInfo.Block;

            var utf8Bytes = new byte[rune.Utf8SequenceLength];
            if (rune.TryEncodeToUtf8(utf8Bytes, out var bytesWritten))
            {
                Array.Resize(ref utf8Bytes, bytesWritten);
                Utf8 = string.Join(" ", utf8Bytes.Select(x => $"0x{x:X2}"));
            }
        }

        public string CodePointValue { get; }

        public UnicodeCategory Category { get; }

        public string Block { get; }

        public string Name { get; }

        public string DisplayText { get; }

        public string? Utf8 { get; }

        public bool IsInteresting =>
            Block != "Basic Latin";
    }
}
