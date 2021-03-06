// Copyright 2007-2010 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Topshelf.Host
{
	using System;
	using System.IO;
	using System.Linq;
	using FileSystem;
	using Shelving;


	public class TopshelfHostService
	{
		ShelfMaker _shelfMaker;

		public void Start()
		{
			_shelfMaker = new ShelfMaker();
			_shelfMaker.MakeShelf("TopShelf.DirectoryWatcher", typeof(DirectoryMonitorBootstrapper));

			string serviceDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Services");

			Directory.GetDirectories(serviceDir)
				.ToList()
                .ConvertAll<string>(Path.GetFileName)
				.ForEach(dir => _shelfMaker.MakeShelf(dir));
		}

		public void Stop()
		{
			_shelfMaker.Dispose();
		}
	}
}