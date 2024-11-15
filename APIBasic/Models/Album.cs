using System;
using System.Collections.Generic;

namespace APIBasic.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public string Title { get; set; } = null!;

    public int ArtistId { get; set; }
}
