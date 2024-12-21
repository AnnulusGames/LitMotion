#if LITMOTION_SUPPORT_PHYSICS

using UnityEngine;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for Rigidbody.
    /// </summary>
    public static class LitMotionRigidbodyExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to Rigidbody.position
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="rigidbody">Target component</param>
        /// <param name="useMovePosition">Whether to use rigidbody.MovePosition()</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPosition<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, Rigidbody rigidbody, bool useMovePosition = true)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(rigidbody);

            if (useMovePosition)
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    rigidbody.MovePosition(x);
                });
            }
            else
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    rigidbody.position = x;
                });
            }
        }

        /// <summary>
        /// Create a motion data and bind it to Rigidbody.position.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="rigidbody">Target component</param>
        /// <param name="useMovePosition">Whether to use rigidbody.MovePosition()</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPositionX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Rigidbody rigidbody, bool useMovePosition = true)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rigidbody);

            if (useMovePosition)
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.x = x;
                    rigidbody.MovePosition(p);
                });
            }
            else
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.x = x;
                    rigidbody.position = p;
                });
            }
        }

        /// <summary>
        /// Create a motion data and bind it to Rigidbody.position.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="rigidbody">Target component</param>
        /// <param name="useMovePosition">Whether to use rigidbody.MovePosition()</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPositionY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Rigidbody rigidbody, bool useMovePosition = true)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rigidbody);

            if (useMovePosition)
            {
                return builder.Bind(rigidbody, static (y, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.y = y;
                    rigidbody.MovePosition(p);
                });
            }
            else
            {
                return builder.Bind(rigidbody, static (y, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.y = y;
                    rigidbody.position = p;
                });
            }
        }

        /// <summary>
        /// Create a motion data and bind it to Rigidbody.position.z
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="rigidbody">Target component</param>
        /// <param name="useMovePosition">Whether to use rigidbody.MovePosition()</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPositionZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Rigidbody rigidbody, bool useMovePosition = true)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rigidbody);

            if (useMovePosition)
            {
                return builder.Bind(rigidbody, static (z, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.z = z;
                    rigidbody.MovePosition(p);
                });
            }
            else
            {
                return builder.Bind(rigidbody, static (z, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.z = z;
                    rigidbody.position = p;
                });
            }
        }

        /// <summary>
        /// Create a motion data and bind it to Rigidbody.position.xy
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="rigidbody">Target component</param>
        /// <param name="useMovePosition">Whether to use rigidbody.MovePosition()</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPositionXY<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, Rigidbody rigidbody, bool useMovePosition = true)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rigidbody);

            if (useMovePosition)
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.x = x.x;
                    p.y = x.y;
                    rigidbody.MovePosition(p);
                });
            }
            else
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.x = x.x;
                    p.y = x.y;
                    rigidbody.position = p;
                });
            }
        }

        /// <summary>
        /// Create a motion data and bind it to Rigidbody.position.yz
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="rigidbody">Target component</param>
        /// <param name="useMovePosition">Whether to use rigidbody.MovePosition()</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPositionYZ<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, Rigidbody rigidbody, bool useMovePosition = true)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rigidbody);

            if (useMovePosition)
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.y = x.x;
                    p.z = x.y;
                    rigidbody.MovePosition(p);
                });
            }
            else
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.y = x.x;
                    p.z = x.y;
                    rigidbody.position = p;
                });
            }
        }


        /// <summary>
        /// Create a motion data and bind it to Rigidbody.position.xz
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="rigidbody">Target component</param>
        /// <param name="useMovePosition">Whether to use rigidbody.MovePosition()</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPositionXZ<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, Rigidbody rigidbody, bool useMovePosition = true)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rigidbody);

            if (useMovePosition)
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.x = x.x;
                    p.z = x.y;
                    rigidbody.MovePosition(p);
                });
            }
            else
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    var p = rigidbody.position;
                    p.x = x.x;
                    p.z = x.y;
                    rigidbody.position = p;
                });
            }
        }

        /// <summary>
        /// Create a motion data and bind it to Rigidbody.rotation
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="rigidbody">Target component</param>
        /// <param name="useMoveRotation">Whether to use rigidbody.MoveRotation()</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToRotation<TOptions, TAdapter>(this MotionBuilder<Quaternion, TOptions, TAdapter> builder, Rigidbody rigidbody, bool useMoveRotation = true)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Quaternion, TOptions>
        {
            Error.IsNull(rigidbody);

            if (useMoveRotation)
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    rigidbody.MoveRotation(x);
                });
            }
            else
            {
                return builder.Bind(rigidbody, static (x, rigidbody) =>
                {
                    rigidbody.rotation = x;
                });
            }
        }
    }
}

#endif