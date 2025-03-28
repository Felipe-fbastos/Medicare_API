using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



public class ResponsavelDTO
{
    public required int IdResponsavel { get; set; }

    public int IdUtilizador { get; set; }

    public required int IdGrauParentesco { get; set; }

    public required DateTime DcResponsavel { get; set; }
    public required DateTime DuResponsavel { get; set; }
    public required string StResponsavel { get; set; }

}

