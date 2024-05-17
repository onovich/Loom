# Prism
Loom, a lightweight UI management library, named after the concept of "weaving". <br/>
**Loom, 一个轻量级的 UI 管理库，取名自“织”。**

![](https://github.com/onovich/Loom/blob/main/Assets/com.tenon.loom/Resources_Sample/codecover_loom_overlay.png)
![](https://github.com/onovich/Loom/blob/main/Assets/com.tenon.loom/Resources_Sample/codecover_loom_worldspace.png)

Loom provides lifecycle management for Unity's UGUI.<br/>
**Loom 为 Unity 的 UGUI 提供生命周期托管。**

The UI supports both single-window and multi-window interfaces.<br/>
**UI 支持单界面窗口，也支持复数界面窗口。**

A single-window interface is restricted to being open only one at a time. Such an interface is suitable for most use cases, and this limitation can prevent many issues.<br/>
**单界面窗口被限定只能同时打开一个。这样的界面适合大多数的应用场景，做这样的限定可以避免很多麻烦。**

The UI can be created in both Overlay and WorldSpace Canvases (you need to attach a WorldSpace Canvas and configure the Canvas Group on the UI prefab).<br/>
**UI 可以创建在 Overlay 的 Canvas 中，也可以创建在 WorldSpace 中（需要自己在 UI 预制体上挂载 WorldSpace 的 Canvas 以及配置 Canvas Group）。**

It is suitable for projects with simple UI requirements, such as demos and game jams.<br/>
**适用于 UI 需求简单的项目，如 Demo 和 GameJam。**

The project provides a wealth of runtime examples.<br/>
**项目内提供了丰富的运行时示例。**

# Readiness
Available, but may undergo refactoring.<br/>
**可用，但不排除会重构。**

# Feature
## To be implemented
Easing Tick

# Sample
```
// Load Assets
public void Init() {
    Canvas mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
    Transform worldSpaceFakeCanvas = GameObject.Find("FakeCanvas").transform;
    Camera worldSpaceCamera = GameObject.Find("MainCamera").GetComponent<Camera>();

    uiCore = new UICore("UI", mainCanvas, worldSpaceFakeCanvas, worldSpaceCamera); // "UI" Is Customized Label Used By Addressable
    Action action = async () => {
        try {
            await uiCore.LoadAssets();
        } catch (Exception e) {
            Debug.Log(e.ToString());
        }
    };
    action.Invoke();
}
```

```
// Open
public T UniquePanel_Open<T>(bool isWorldSpace = false) where T : IPanel {
    return uiCore.UniquePanel_Open<T>(isWorldSpace);
}

public T MultiplePanel_Open<T>(bool isWorldSpace) where T : IPanel {
    var index = MultiPanel_PickNextEmptyIndex();
    if (index == -1) {
        return default(T);
    }
    var panel = uiCore.MultiplePanel_Open<T>(isWorldSpace);
    MultiplePanel_RecordIndex(panel, index);
    return panel;
}
```
```
// Get
public T UniquePanel_Get<T>() where T : IPanel {
    return uiCore.UniquePanel_Get<T>();
}

public bool UniquePanel_TryGet<T>(out T panel) where T : IPanel {
    return uiCore.UniquePanel_TryGet<T>(out panel);
}
```
```
// ForEach
public void MultiplePanel_GroupForEach<T>(Action<T> action) where T : IPanel {
    uiCore.MultiplePanel_GroupForEach<T>(action);
}
```
```
// Close
public void UniquePanel_Close<T>() where T : IPanel {
    uiCore.UniquePanel_Close<T>();
}

public void MultiplePanel_Close<T>(T panel) where T : IPanel {
    uiCore.MultiplePanel_Close<T>(panel);
}

public void MultiplePanel_CloseGroup<T>() where T : IPanel {
    MultiplePanel_ClearAllIndexRecord();
    uiCore.MultiplePanel_CloseGroup<T>();
}
```
```
// TearDown And Release
public void ReleaseAssets() {
    uiCore.ReleaseAssets();
}

public void TearDown() {
    uiCore.Clear();
}
```

# Project Sample
[Oshi](https://github.com/onovich/Oshi) A Sokobanlike Game In Development.

[Leap](https://github.com/onovich/Leap) A 2D Platform Jumping Game In Development.

[Alter](https://github.com/onovich/Alter) A Tetrislike Game In Development.

# Dependency
Unity Addressable

# UPM URL
ssh://git@github.com/onovich/Loom.git?path=/Assets/com.tenon.loom#main
