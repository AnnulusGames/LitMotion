using System.Linq;
using UnityEngine;
using UnityEditor;

namespace LitMotion.Editor
{
    /// <summary>
    /// Editor window that displays a list of motions being tracked.
    /// </summary>
    public class MotionDebuggerWindow : EditorWindow
    {
        static MotionDebuggerWindow instance;

        [MenuItem("Window/LitMotion Debugger")]
        public static void OpenWindow()
        {
            if (instance != null) instance.Close();
            GetWindow<MotionDebuggerWindow>("LitMotion Debugger").Show();
        }

        static readonly GUILayoutOption[] EmptyLayoutOption = new GUILayoutOption[0];

        MotionDebuggerTreeView treeView;
        object splitterState;

        const string EnableTrackingPrefsKey = "LitMotion-MotionTracker-EnableTracking";
        const string EnableStackTracePrefsKey = "LitMotion-MotionTracker-EnableStackTrace";

        void OnEnable()
        {
            instance = this;
            splitterState = SplitterGUILayout.CreateSplitterState(new float[] { 75f, 25f }, new int[] { 32, 32 }, null);
            treeView = new MotionDebuggerTreeView();
            MotionDebugger.EnableTracking = EditorPrefs.GetBool(EnableTrackingPrefsKey, false);
            MotionDebugger.EnableStackTrace = EditorPrefs.GetBool(EnableStackTracePrefsKey, false);
        }

        void OnGUI()
        {
            RenderHeadPanel();
            SplitterGUILayout.BeginVerticalSplit(this.splitterState, EmptyLayoutOption);
            RenderTable();
            RenderDetailsPanel();
            SplitterGUILayout.EndVerticalSplit();
        }

        static readonly GUIContent ClearHeadContent = EditorGUIUtility.TrTextContent(" Clear ");
        static readonly GUIContent EnableTrackingHeadContent = EditorGUIUtility.TrTextContent("Enable Tracking");
        static readonly GUIContent EnableStackTraceHeadContent = EditorGUIUtility.TrTextContent("Enable Stack Trace");

        void RenderHeadPanel()
        {
            EditorGUILayout.BeginVertical(EmptyLayoutOption);
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, EmptyLayoutOption);

            if (GUILayout.Toggle(MotionDebugger.EnableTracking, EnableTrackingHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption) != MotionDebugger.EnableTracking)
            {
                MotionDebugger.EnableTracking = !MotionDebugger.EnableTracking;
                EditorPrefs.SetBool(EnableTrackingPrefsKey, MotionDebugger.EnableTracking);
            }

            if (GUILayout.Toggle(MotionDebugger.EnableStackTrace, EnableStackTraceHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption) != MotionDebugger.EnableStackTrace)
            {
                MotionDebugger.EnableStackTrace = !MotionDebugger.EnableStackTrace;
                EditorPrefs.SetBool(EnableStackTracePrefsKey, MotionDebugger.EnableStackTrace);
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button(ClearHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption))
            {
                MotionDebugger.Clear();
                treeView.ReloadAndSort();
                Repaint();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        Vector2 tableScroll;
        GUIStyle tableListStyle;

        void RenderTable()
        {
            if (tableListStyle == null)
            {
                tableListStyle = new GUIStyle("CN Box");
                tableListStyle.margin.top = 0;
                tableListStyle.padding.left = 3;
            }

            EditorGUILayout.BeginVertical(tableListStyle, EmptyLayoutOption);

            tableScroll = EditorGUILayout.BeginScrollView(this.tableScroll, new GUILayoutOption[]
            {
                GUILayout.ExpandWidth(true),
                GUILayout.MaxWidth(2000f)
            });
            var controlRect = EditorGUILayout.GetControlRect(new GUILayoutOption[]
            {
                GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true)
            });

            treeView?.OnGUI(controlRect);

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        static int interval;
        void Update()
        {
            if (interval++ % 120 == 0)
            {
                treeView.ReloadAndSort();
                Repaint();
            }
        }

        static GUIStyle detailsStyle;
        Vector2 detailsScroll;

        void RenderDetailsPanel()
        {
            if (detailsStyle == null)
            {
                detailsStyle = new GUIStyle("CN Message")
                {
                    wordWrap = false,
                    stretchHeight = true
                };
                detailsStyle.margin.right = 15;
            }

            var selected = treeView.state.selectedIDs;

            if (selected.Count <= 0) return;
            var first = selected[0];

            if (treeView.CurrentBindingItems.FirstOrDefault(x => x.id == first) is not MotionDebuggerViewItem item) return;

            detailsScroll = EditorGUILayout.BeginScrollView(this.detailsScroll, tableListStyle, EmptyLayoutOption);
            {
                ref var unmanagedData = ref MotionManager.GetDataRef(item.Handle);
                ref var managedData = ref MotionManager.GetManagedDataRef(item.Handle);
                var debugInfo = MotionManager.GetDebugInfo(item.Handle);

                GenericField("Name", managedData.DebugName);
                EditorGUILayout.Space(2);

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    EditorGUILayout.LabelField("Motion Data", EditorStyles.boldLabel);

                    GenericField("Start Value", debugInfo.StartValue);
                    GenericField("End Value", debugInfo.EndValue);

                    EditorGUILayout.Space(2);
                    GenericField("Duration", unmanagedData.Duration);
                    GenericField("Delay", unmanagedData.Delay);
                    GenericField("Delay Type", unmanagedData.DelayType);
                    GenericField("Loops", unmanagedData.Loops);
                    GenericField("Loop Type", unmanagedData.LoopType);

                    EditorGUILayout.Space(2);
                    GenericField("Ease", unmanagedData.Ease);
                    if (unmanagedData.Ease is Ease.CustomAnimationCurve)
                    {
                        GenericField("Custom Ease Curve", unmanagedData.AnimationCurve);
                    }

                    EditorGUILayout.Space(2);
                    GenericField("Type", item.MotionType);
                    GenericField("Scheduler", item.SchedulerType);
                    GenericField("Cancel On Error", managedData.CancelOnError);
                    GenericField("Skip Values During Delay", managedData.SkipValuesDuringDelay);

                    EditorGUILayout.Space(2);
                    GenericField("State[0]", managedData.State0);
                    GenericField("State[1]", managedData.State1);
                    GenericField("State[2]", managedData.State2);
                }

                EditorGUILayout.Space(2);

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    EditorGUILayout.LabelField("Status", EditorStyles.boldLabel);
                    EditorGUILayout.LabelField("Status", unmanagedData.Status.ToString());
                    GenericField("Time", unmanagedData.Time);
                    GenericField("Completed Loops", unmanagedData.ComplpetedLoops);
                    EditorGUILayout.Space(2);
                    GenericField("Playback Speed", unmanagedData.PlaybackSpeed);
                    GenericField("Is Preserved", unmanagedData.IsPreserved);
                }

                EditorGUILayout.Space(2);

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    EditorGUILayout.LabelField("Stack Trace", EditorStyles.boldLabel);

                    var vector = detailsStyle.CalcSize(new GUIContent(item.StackTrace));
                    EditorGUILayout.SelectableLabel(item.StackTrace, detailsStyle, new GUILayoutOption[]
                    {
                        GUILayout.ExpandWidth(true),
                        GUILayout.MinWidth(vector.x),
                        GUILayout.MinHeight(vector.y)
                    });
                }
            }
            EditorGUILayout.EndScrollView();
        }

        static void GenericField<T>(string label, T value)
        {
            if (value is null) return;

            switch (value)
            {
                default:
                    EditorGUILayout.LabelField(label, value.ToString());
                    break;
                case UnityEngine.AnimationCurve v:
                    EditorGUILayout.CurveField(label, v);
                    break;
                case UnityEngine.Object v:
                    EditorGUILayout.ObjectField(label, v, v.GetType(), true);
                    break;
            }
        }
    }
}