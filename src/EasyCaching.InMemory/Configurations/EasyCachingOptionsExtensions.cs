﻿namespace EasyCaching.InMemory
{
    using EasyCaching.Core;
    using Microsoft.Extensions.Configuration;
    using System;

    /// <summary>
    /// EasyCaching options extensions of InMemory.
    /// </summary>
    public static class EasyCachingOptionsExtensions
    {
        /// <summary>
        /// Uses the in memory.
        /// </summary>
        /// <returns>The in memory.</returns>
        /// <param name="options">Options.</param>
        /// <param name="name">Name.</param>
        public static EasyCachingOptions UseInMemory(this EasyCachingOptions options, string name = "")
        {
            var option = new InMemoryOptions();

            void configure(InMemoryOptions x)
            {
                x.CachingProviderType = option.CachingProviderType;
                x.EnableLogging = option.EnableLogging;
                x.MaxRdSecond = option.MaxRdSecond;
                x.Order = option.Order;
            }

            options.RegisterExtension(new InMemoryOptionsExtension(name, configure));

            return options;
        }

        /// <summary>
        /// Uses the in memory.
        /// </summary>
        /// <returns>The in memory.</returns>
        /// <param name="options">Options.</param>
        /// <param name="configure">Configure.</param>
        /// <param name="name">Name.</param>
        public static EasyCachingOptions UseInMemory(this EasyCachingOptions options, Action<InMemoryOptions> configure, string name = "")
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            options.RegisterExtension(new InMemoryOptionsExtension(name, configure));

            return options;
        }

        /// <summary>
        /// Uses the in memory.
        /// </summary>
        /// <returns>The in memory.</returns>
        /// <param name="options">Options.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="name">Name.</param>
        public static EasyCachingOptions UseInMemory(this EasyCachingOptions options, IConfiguration configuration, string name = "")
        {
            var dbConfig = configuration.GetSection(EasyCachingConstValue.InMemorySection);
            var memoryOptions = new InMemoryOptions();
            dbConfig.Bind(memoryOptions);

            void configure(InMemoryOptions x)
            {
                x.CachingProviderType = memoryOptions.CachingProviderType;
                x.EnableLogging = memoryOptions.EnableLogging;
                x.MaxRdSecond = memoryOptions.MaxRdSecond;
                x.Order = memoryOptions.Order;
            }

            options.RegisterExtension(new InMemoryOptionsExtension(name, configure));

            return options;
        }
    }
}
