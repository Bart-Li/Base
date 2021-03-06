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

namespace Org.Apache.Zookeeper.Txn
{
    public class CreateTxn : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(CreateTxn));

        public CreateTxn()
        {
        }

        public CreateTxn(
        string path
      ,
        byte[] data
      ,
        System.Collections.Generic.IEnumerable<Org.Apache.Zookeeper.Data.ACL> acl
      ,
        bool ephemeral
      ,
        int parentCVersion
      )
        {
            Path = path;
            Data = data;
            Acl = acl;
            Ephemeral = ephemeral;
            ParentCVersion = parentCVersion;
        }

        public string Path { get; set; }
        public byte[] Data { get; set; }
        public System.Collections.Generic.IEnumerable<Org.Apache.Zookeeper.Data.ACL> Acl { get; set; }
        public bool Ephemeral { get; set; }
        public int ParentCVersion { get; set; }

        public void Serialize(IOutputArchive a_, String tag)
        {
            a_.StartRecord(this, tag);
            a_.WriteString(Path, "path");
            a_.WriteBuffer(Data, "data");
            {
                a_.StartVector(Acl, "acl");
                if (Acl != null)
                {
                    foreach (var e1 in Acl)
                    {
                        a_.WriteRecord(e1, "e1");
                    }
                }
                a_.EndVector(Acl, "acl");
            }
            a_.WriteBool(Ephemeral, "ephemeral");
            a_.WriteInt(ParentCVersion, "parentCVersion");
            a_.EndRecord(this, tag);
        }

        public void Deserialize(IInputArchive a_, String tag)
        {
            a_.StartRecord(tag);
            Path = a_.ReadString("path");
            Data = a_.ReadBuffer("data");
            {
                IIndex vidx1 = a_.StartVector("acl");
                if (vidx1 != null)
                {
                    var tmpLst = new System.Collections.Generic.List<Org.Apache.Zookeeper.Data.ACL>();
                    for (; !vidx1.Done(); vidx1.Incr())
                    {
                        Org.Apache.Zookeeper.Data.ACL e1;
                        e1 = new Org.Apache.Zookeeper.Data.ACL();
                        a_.ReadRecord(e1, "e1");
                        tmpLst.Add(e1);
                    }
                    Acl = tmpLst;
                }
                a_.EndVector("acl");
            }
            Ephemeral = a_.ReadBool("ephemeral");
            ParentCVersion = a_.ReadInt("parentCVersion");
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
                    a_.WriteString(Path, "path");
                    a_.WriteBuffer(Data, "data");
                    {
                        a_.StartVector(Acl, "acl");
                        if (Acl != null)
                        {
                            foreach (var e1 in Acl)
                            {
                                a_.WriteRecord(e1, "e1");
                            }
                        }
                        a_.EndVector(Acl, "acl");
                    }
                    a_.WriteBool(Ephemeral, "ephemeral");
                    a_.WriteInt(ParentCVersion, "parentCVersion");
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
            throw new InvalidOperationException("comparing CreateTxn is unimplemented");
        }

        public override bool Equals(object obj)
        {
            CreateTxn peer = (CreateTxn)obj;
            if (peer == null)
            {
                return false;
            }
            if (Object.ReferenceEquals(peer, this))
            {
                return true;
            }
            bool ret = false;
            ret = Path.Equals(peer.Path);
            if (!ret) return ret;
            ret = Data.Equals(peer.Data);
            if (!ret) return ret;
            ret = Acl.Equals(peer.Acl);
            if (!ret) return ret;
            ret = (Ephemeral == peer.Ephemeral);
            if (!ret) return ret;
            ret = (ParentCVersion == peer.ParentCVersion);
            if (!ret) return ret;
            return ret;
        }

        public override int GetHashCode()
        {
            int result = 17;
            int ret = GetType().GetHashCode();
            result = 37 * result + ret;
            ret = Path.GetHashCode();
            result = 37 * result + ret;
            ret = Data.GetHashCode();
            result = 37 * result + ret;
            ret = Acl.GetHashCode();
            result = 37 * result + ret;
            ret = (Ephemeral) ? 0 : 1;
            result = 37 * result + ret;
            ret = (int)ParentCVersion;
            result = 37 * result + ret;
            return result;
        }

        public static string Signature()
        {
            return "LCreateTxn(sB[LACL(iLId(ss))]zi)";
        }
    }
}