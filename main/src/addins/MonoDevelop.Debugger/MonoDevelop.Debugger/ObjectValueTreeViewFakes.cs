//
// ObjectValueTreeViewFakes.cs
//
// Author:
//       gregm <gregm@microsoft.com>
//
// Copyright (c) 2019 Microsoft
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MonoDevelop.Debugger
{
	/// <summary>
	/// An IObjectValueNode used for debugging
	/// </summary>
	abstract class DebugObjectValueNode : AbstractObjectValueNode
	{
		protected DebugObjectValueNode (string parentPath, string name) : base (parentPath, name)
		{
		}

		public override bool HasChildren => true;

		public override string Value => "none";
		public override string TypeName => "No Type";
		public override string DisplayValue => "dummy";
	}

	/// <summary>
	/// An IObjectValueNode used for debugging
	/// </summary>
	sealed class FakeObjectValueNode : DebugObjectValueNode
	{
		public FakeObjectValueNode (string parentPath) : base (parentPath, "fake")
		{
		}

		public override bool HasChildren => true;

		public override string Value => "none";
		public override string TypeName => "No Type";
		public override string DisplayValue => "dummy";


		protected override async Task<IEnumerable<IObjectValueNode>> OnLoadChildrenAsync (CancellationToken cancellationToken)
		{
			// TODO: do some sleeping...
			await Task.Delay (1000);
			return new [] { new FakeObjectValueNode (this.Path) };
		}
	}

	/// <summary>
	/// An IObjectValueNode used for debugging
	/// </summary>
	sealed class FakeEvaluatingObjectValueNode : DebugObjectValueNode
	{
		bool isEvaluating;
		bool hasChildren;
		public FakeEvaluatingObjectValueNode (string parentPath) : base (parentPath, "evaluating")
		{
			this.isEvaluating = true;
			DoTest ();
		}

		public override bool HasChildren => hasChildren;
		public override bool IsEvaluating => isEvaluating;

		public override string Value => "none";
		public override string TypeName => "No Type";
		public override string DisplayValue => "dummy";


		protected override async Task<IEnumerable<IObjectValueNode>> OnLoadChildrenAsync (CancellationToken cancellationToken)
		{
			// TODO: do some sleeping...
			await Task.Delay (1000);

			return new [] { new FakeObjectValueNode (this.Path) };
		}

		async void DoTest ()
		{
			await Task.Delay (3000);
			this.isEvaluating = false;
			this.hasChildren = true;
			this.OnValueChanged (EventArgs.Empty);
		}
	}
}
