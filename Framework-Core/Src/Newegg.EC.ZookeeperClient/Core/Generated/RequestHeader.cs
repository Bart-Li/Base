// File generated by hadoop record compiler. Do not edit.
using log4net;
using Org.Apache.Jute;

/**
* Licensed to the Apache Software Foundation (ASF) under one
* or more contributor license agreements.  See the NOTICE file
* distributed with this work for additional information
* regarding copyright ownership.  The ASF licenses this file
* to you under the Apache License, Version 2.0 (the
* "License"); you may not use this file except in compliance
* with the License.  You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;

namespace Org.Apache.Zookeeper.Proto
{
    public class RequestHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(RequestHeader));

        public RequestHeader()
        {
        }

        public RequestHeader(
        int xid
      ,
        int type
      )
        {
            Xid = xid;
            Type = type;
        }

        public int Xid { get; set; }
        public int Type { get; set; }

        public void Serialize(IOutputArchive a_, String tag)
        {
            a_.StartRecord(this, tag);
            a_.WriteInt(Xid, "xid");
            a_.WriteInt(Type, "type");
            a_.EndRecord(this, tag);
        }

        public void Deserialize(IInputArchive a_, String tag)
        {
            a_.StartRecord(tag);
            Xid = a_.ReadInt("xid");
            Type = a_.ReadInt("type");
            a_.EndRecord(tag);
        }

        public override String ToString()
        {
            try
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (Newegg.EC.Zookeeper.Client.Core.IO.EndianBinaryWriter writer =
                  new Newegg.EC.Zookeeper.Client.Core.IO.EndianBinaryWriter(Newegg.EC.Zookeeper.Client.Core.IO.EndianBitConverter.Big, ms, System.Text.Encoding.UTF8))
                {
                    BinaryOutputArchive a_ =
                      new BinaryOutputArchive(writer);
                    a_.StartRecord(this, string.Empty);
                    a_.WriteInt(Xid, "xid");
                    a_.WriteInt(Type, "type");
                    a_.EndRecord(this, string.Empty);
                    ms.Position = 0;
                    return System.Text.Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
            return "ERROR";
        }

        public void Write(Newegg.EC.Zookeeper.Client.Core.IO.EndianBinaryWriter writer)
        {
            BinaryOutputArchive archive = new BinaryOutputArchive(writer);
            Serialize(archive, string.Empty);
        }

        public void ReadFields(Newegg.EC.Zookeeper.Client.Core.IO.EndianBinaryReader reader)
        {
            BinaryInputArchive archive = new BinaryInputArchive(reader);
            Deserialize(archive, string.Empty);
        }

        public int CompareTo(object obj)
        {
            RequestHeader peer = (RequestHeader)obj;
            if (peer == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int ret = 0;
            ret = (Xid == peer.Xid) ? 0 : ((Xid < peer.Xid) ? -1 : 1);
            if (ret != 0) return ret;
            ret = (Type == peer.Type) ? 0 : ((Type < peer.Type) ? -1 : 1);
            if (ret != 0) return ret;
            return ret;
        }

        public override bool Equals(object obj)
        {
            RequestHeader peer = (RequestHeader)obj;
            if (peer == null)
            {
                return false;
            }
            if (Object.ReferenceEquals(peer, this))
            {
                return true;
            }
            bool ret = false;
            ret = (Xid == peer.Xid);
            if (!ret) return ret;
            ret = (Type == peer.Type);
            if (!ret) return ret;
            return ret;
        }

        public override int GetHashCode()
        {
            int result = 17;
            int ret = GetType().GetHashCode();
            result = 37 * result + ret;
            ret = (int)Xid;
            result = 37 * result + ret;
            ret = (int)Type;
            result = 37 * result + ret;
            return result;
        }

        public static string Signature()
        {
            return "LRequestHeader(ii)";
        }
    }
}