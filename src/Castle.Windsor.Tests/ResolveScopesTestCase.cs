// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace CastleTests
{
	using Castle.MicroKernel.Lifestyle.Scoped;
	using Castle.MicroKernel.Registration;

	using CastleTests.Components;

	using NUnit.Framework;

	[TestFixture]
	public class ResolveScopesTestCase : AbstractContainerTestCase
	{
		protected override void AfterContainerCreated()
		{
			Kernel.AddSubSystem("scope", new ScopeSubsystem(new ThreadScopeAccessor()));
		}

		[Test]
		public void Resolve_scope_should_scope_lifetime_of_transient_components()
		{
			Container.Register(Component.For<ADisposable>().LifeStyle.Transient);
			ADisposable a;
			using(Container.BeginScope())
			{
				a = Container.Resolve<ADisposable>();
				Assert.False(a.Disposed);
			}
			Assert.True(a.Disposed);
		}
	}
}