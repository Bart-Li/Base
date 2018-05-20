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

namespace Org.Apache.Zookeeper.Server.Quorum
{
    public class QuorumPacket : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(QuorumPacket));

        public QuorumPacket()
        {
        }

        public QuorumPacket(
        int type
      ,
        long zxid
      ,
        byte[] data
      ,
        System.Collections.Generic.IEnumerable<Org.Apache.Zookeeper.Data.ZKId> authinfo
      )
        {
            Type = type;
            Zxid = zxid;
            Data = data;
            Authinfo = authinfo;
        }

        public int Type { get; set; }
        public long Zxid { get; set; }
        public byte[] Data { get; set; }
        public System.Collections.Generic.IEnumerable<Org.Apache.Zookeeper.Data.ZKId> Authinfo { get; set; }

        public void Serialize(IOutputArchive a_, String tag)
        {
            a_.StartRecord(this, tag);
            a_.WriteInt(Type, "type");
            a_.WriteLong(Zxid, "zxid");
            a_.WriteBuffer(Data, "data");
            {
                a_.StartVector(Authinfo, "authinfo");
                if (Authinfo != null)
                {
                    foreach (var e1 in Authinfo)
                    {
                        a_.WriteRecord(e1, "e1");
                    }
                }
                a_.EndVector(Authinfo, "authinfo");
            }
            a_.EndRecord(this, tag);
        }

        public void Deserialize(IInputArchive a_, String tag)
        {
            a_.StartRecord(tag);
            Type = a_.ReadInt("type");
            Zxid = a_.ReadLong("zxid");
            Data = a_.ReadBuffer("data");
            {
                IIndex vidx1 = a_.StartVector("authinfo");
                if (vidx1 != null)
                {
                    var tmpLst = new System.Collections.Generic.List<Org.Apache.Zookeeper.Data.ZKId>();
                    for (; !vidx1.Done(); vidx1.Incr())
                    {
                        Org.Apache.Zookeeper.Data.ZKId e1;
                        e1 = new Org.Apache.Zookeeper.Data.ZKId();
                        a_.ReadRecord(e1, "e1");
                        tmpLst.Add(e1);
                    }
                    Authinfo = tmpLst;
                }
                a_.EndVector("authinfo");
            }
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
                    a_.WriteInt(Type, "type");
                    a_.WriteLong(Zxid, "zxid");
                    a_.WriteBuffer(Data, "data");
                    {
                        a_.StartVector(Authinfo, "authinfo");
                        if (Authinfo != null)
                        {
                            foreach (var e1 in Authinfo)
                            {
                                a_.WriteRecord(e1, "e1");
                            }
                        }
                        a_.EndVector(Authinfo, "authinfo");
                    }
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
            throw new InvalidOperationException("comparing QuorumPacket is unimplemented");
        }

        public override bool Equals(object obj)
        {
            QuorumPacket peer = (QuorumPacket)obj;
            if (peer == null)
            {
                return false;
            }
            if (Object.ReferenceEquals(peer, this))
            {
                return true;
            }
            bool ret = false;
            ret = (Type == peer.Type);
            if (!ret) return ret;
            ret = (Zxid == peer.Zxid);
            if (!ret) return ret;
            ret = Data.Equals(peer.Data);
            if (!ret) return ret;
            ret = Authinfo.Equals(peer.Authinfo);
            if (!ret) return ret;
            return ret;
        }

        public override int GetHashCode()
        {
            int result = 17;
            int ret = GetType().GetHashCode();
            result = 37 * result + ret;
            ret = (int)Type;
            result = 37 * result + ret;
            ret = (int)Zxid;
            result = 37 * result + ret;
            ret = Data.GetHashCode();
            result = 37 * result + ret;
            ret = Authinfo.GetHashCode();
            result = 37 * result + ret;
            return result;
        }

        public static string Signature()
        {
            return "LQuorumPacket(ilB[LId(ss)])";
        }
    }
}