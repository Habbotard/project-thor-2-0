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

using ThorServer.Utilities;
using ThorServer.Game.Users;

namespace ThorServer.Core
{
    public partial class Reactor
    {
        protected ClientMessage mMessage = new ClientMessage();
        
        public UserInfo mUserInfo;
        public int mHeaderID;
        public string mHeaderBody;
        public long mSessionID;
        public string mPacketBody;
    }
}
