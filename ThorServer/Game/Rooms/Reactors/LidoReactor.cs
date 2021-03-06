﻿/*
Thor Server Project
Copyright 2008 Joe Hegarty


This file is part of The Thor Server Project.

The Thor Server Project is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

The Thor Server Project is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with The Thor Server Project.  If not, see <http://www.gnu.org/licenses/>.
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ThorServer.Game.Rooms;
using ThorServer.Utilities;
using ThorServer.Game.Furni;

namespace ThorServer.Game.Rooms.Reactors
{
    public class LidoReactor : PoolReactor
    {
        //104 - "PELLEHYPPY": "Ah" - POOLVOTE
        public void Listener104()
        {
            int signCode;
            if (int.TryParse(mPacketBody, out signCode))
            {
                ApplyUniqueStatus("sign", 2, signCode.ToString(), false, 0, 0, null);
            }
        }
    }
}
