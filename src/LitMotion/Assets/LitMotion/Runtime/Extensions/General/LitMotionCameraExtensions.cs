using UnityEngine;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for Camera
    /// </summary>
    public static class LitMotionCameraExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to Camera.aspect
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAspect<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Camera camera)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(camera);
            return builder.Bind(camera, static (x, camera) =>
            {
                camera.aspect = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Camera.nearClipPlane
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToNearClipPlane<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Camera camera)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(camera);
            return builder.Bind(camera, static (x, camera) =>
            {
                camera.nearClipPlane = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Camera.farClipPlane
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToFarClipPlane<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Camera camera)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(camera);
            return builder.Bind(camera, static (x, camera) =>
            {
                camera.farClipPlane = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Camera.fieldOfView
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToFieldOfView<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Camera camera)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(camera);
            return builder.Bind(camera, static (x, camera) =>
            {
                camera.fieldOfView = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Camera.orthographicSize
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToOrthographicSize<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Camera camera)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(camera);
            return builder.Bind(camera, static (x, camera) =>
            {
                camera.orthographicSize = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Camera.rect
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToRect<TOptions, TAdapter>(this MotionBuilder<Rect, TOptions, TAdapter> builder, Camera camera)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Rect, TOptions>
        {
            Error.IsNull(camera);
            return builder.Bind(camera, static (x, camera) =>
            {
                camera.rect = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Camera.pixelRect
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPixelRect<TOptions, TAdapter>(this MotionBuilder<Rect, TOptions, TAdapter> builder, Camera camera)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Rect, TOptions>
        {
            Error.IsNull(camera);
            return builder.Bind(camera, static (x, camera) =>
            {
                camera.pixelRect = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Camera.backgroundColor
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToBackgroundColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, Camera camera)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(camera);
            return builder.Bind(camera, static (x, camera) =>
            {
                camera.backgroundColor = x;
            });
        }
    }
}