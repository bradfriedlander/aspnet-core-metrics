using System;
using System.Collections.Generic;

namespace demoWebReact.Models
{
    public class DefinitionPacket

    {
        public List<DefinitionModel> Definitions { get; set; }

        public Guid PacketId { get; set; }
    }
}
