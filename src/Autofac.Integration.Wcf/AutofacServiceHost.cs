﻿// Contributed 2008-01-15 by Petar Andrijasevic
// Copyright (c) 2008 Autofac Contributors
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.ServiceModel;

namespace Autofac.Integration.Wcf
{
    /// <summary>
    /// ServiceHost with AutfacDependencyInjectionServiceBehaviour.
    /// </summary>
	public class AutofacServiceHost : ServiceHost
	{
		private readonly Container _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacServiceHost"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        public AutofacServiceHost(Container container, Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
            if (container == null)
                throw new ArgumentNullException("container");

			this._container = container;
		}

        /// <summary>
        /// Invoked during the transition of a communication object into the opening state.
        /// </summary>
		protected override void OnOpening()
		{
			Description.Behaviors.Add(new AutofacDependencyInjectionServiceBehavior(_container));
            // TODO: Support for injecting IServiceBehavior & IEndpointBehavior services registered with the container
			base.OnOpening();
		}
	}
}