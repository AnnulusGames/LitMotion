using System.Linq;
using UnityEngine;
using UnityEditor;

namespace LitMotion.Editor
{
    /// <summary>
    /// Editor window that displays a list of motions being tracked.
    /// </summary>
    public class MotionTrackerWindow : EditorWindow
    {
        static MotionTrackerWindow instance;

        [MenuItem("Window/LitMotion/Motion Tracker")]
        public static void OpenWindow()
        {
            if (instance != null) instance.Close();
            GetWindow<MotionTrackerWindow>("Motion Tracker").Show();
        }

        static readonly GUILayoutOption[] EmptyLayoutOption = new GUILayoutOption[0];

        MotionTrackerTreeView treeView;
        object splitterState;

        const string EnableTrackingPrefsKey = "LitMotion-MotionTracker-EnableTracking";
        const string EnableStackTracePrefsKey = "LitMotion-MotionTracker-EnableStackTrace";

        void OnEnable()
        {
            instance = this;
            splitterState = SplitterGUILayout.CreateSplitterState(new float[] { 75f, 25f }, new int[] { 32, 32 }, null);
            treeView = new MotionTrackerTreeView();
            MotionTracker.EnableTracking = EditorPrefs.GetBool(EnableTrackingPrefsKey, false);
            MotionTracker.EnableStackTrace = EditorPrefs.GetBool(EnableStackTracePrefsKey, false);
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

            if (GUILayout.Toggle(MotionTracker.EnableTracking, EnableTrackingHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption) != MotionTracker.EnableTracking)
            {
                MotionTracker.EnableTracking = !MotionTracker.EnableTracking;
                EditorPrefs.SetBool(EnableTrackingPrefsKey, MotionTracker.EnableTracking);
            }

            if (GUILayout.Toggle(MotionTracker.EnableStackTrace, EnableStackTraceHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption) != MotionTracker.EnableStackTrace)
            {
                MotionTracker.EnableStackTrace = !MotionTracker.EnableStackTrace;
                EditorPrefs.SetBool(EnableStackTracePrefsKey, MotionTracker.EnableStackTrace);
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button(ClearHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption))
            {
                MotionTracker.Clear();
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

            string message = "";
            var selected = treeView.state.selectedIDs;
            if (selected.Count > 0)
            {
                var first = selected[0];
                if (treeView.CurrentBindingItems.FirstOrDefault(x => x.id == first) is MotionTrackerViewItem item)
                {
                    message = item.Position;
                }
            }

            detailsScroll = EditorGUILayout.BeginScrollView(this.detailsScroll, EmptyLayoutOption);
            var vector = detailsStyle.CalcSize(new GUIContent(message));
            EditorGUILayout.SelectableLabel(message, detailsStyle, new GUILayoutOption[]
            {
                GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true),
                GUILayout.MinWidth(vector.x),
                GUILayout.MinHeight(vector.y)
            });
            EditorGUILayout.EndScrollView();
        }
    }
}