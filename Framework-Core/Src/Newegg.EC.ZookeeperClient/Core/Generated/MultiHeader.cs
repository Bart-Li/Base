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
    public class MultiHeader : IRecord, IComparable
    {
        private static ILog log = LogManager.GetLogger(typeof(MultiHeader));

        public MultiHeader()
        {
        }

        public MultiHeader(
        int type
      ,
        bool done
      ,
        int err
      )
        {
            Type = type;
            Done = done;
            Err = err;
        }

        public int Type { get; set; }
        public bool Done { get; set; }
        public int Err { get; set; }

        public void Serialize(IOutputArchive a_, String tag)
        {
            a_.StartRecord(this, tag);
            a_.WriteInt(Type, "type");
            a_.WriteBool(Done, "done");
            a_.WriteInt(Err, "err");
            a_.EndRecord(this, tag);
        }

        public void Deserialize(IInputArchive a_, String tag)
        {
            a_.StartRecord(tag);
            Type = a_.ReadInt("type");
            Done = a_.ReadBool("done");
            Err = a_.ReadInt("err");
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
                    a_.WriteBool(Done, "done");
                    a_.WriteInt(Err, "err");
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
            MultiHeader peer = (MultiHeader)obj;
            if (peer == null)
            {
                throw new InvalidOperationException("Comparing different types of records.");
            }
            int ret = 0;
            ret = (Type == peer.Type) ? 0 : ((Type < peer.Type) ? -1 : 1);
            if (ret != 0) return ret;
            ret = (Done == peer.Done) ? 0 : (Done ? 1 : -1);
            if (ret != 0) return ret;
            ret = (Err == peer.Err) ? 0 : ((Err < peer.Err) ? -1 : 1);
            if (ret != 0) return ret;
            return ret;
        }

        public override bool Equals(object obj)
        {
            MultiHeader peer = (MultiHeader)obj;
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
            ret = (Done == peer.Done);
            if (!ret) return ret;
            ret = (Err == peer.Err);
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
            ret = (Done) ? 0 : 1;
            result = 37 * result + ret;
            ret = (int)Err;
            result = 37 * result + ret;
            return result;
        }

        public static string Signature()
        {
            return "LMultiHeader(izi)";
        }
    }
}