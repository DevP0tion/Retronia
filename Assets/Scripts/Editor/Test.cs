using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class MyGraphEditorWindow : EditorWindow
{
  [MenuItem("Window/Custom Graph")]
  public static void Open()
  {
    var window = GetWindow<MyGraphEditorWindow>();
    window.titleContent = new GUIContent("My Graph");
  }

  private void OnEnable()
  {
    var graphView = new MyGraphView
    {
      name = "My Graph"
    };
    graphView.StretchToParentSize();
    rootVisualElement.Add(graphView);
  }
}

public class MyGraphView : GraphView
{
  public MyGraphView()
  {
    this.AddManipulator(new ContentDragger());
    this.AddManipulator(new SelectionDragger());
    this.AddManipulator(new RectangleSelector());

    this.SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

    var grid = new GridBackground();
    Insert(0, grid);
    grid.StretchToParentSize();

    AddElement(CreateNode("Start Node", new Vector2(100, 200)));
  }

  public Node CreateNode(string title, Vector2 position)
  {
    var node = new Node
    {
      title = title,
      style = { top = position.y, left = position.x }
    };

    var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
    inputPort.portName = "In";
    node.inputContainer.Add(inputPort);

    var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
    outputPort.portName = "Out";
    node.outputContainer.Add(outputPort);

    node.RefreshExpandedState();
    node.RefreshPorts();

    return node;
  }
}
