namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia
open Avalonia.Controls
open Avalonia.Controls.Primitives
open Avalonia.Controls.Primitives.PopupPositioning
open Fabulous
open Fabulous.StackAllocatedCollections.StackList

type IFabPopup =
    inherit IFabControl

module Popup =
    let WidgetKey = Widgets.register<Popup>()

    let WindowManagerAddShadowHint =
        Attributes.defineAvaloniaPropertyWithEquality Popup.WindowManagerAddShadowHintProperty

    let Child = Attributes.defineAvaloniaPropertyWidget Popup.ChildProperty

    let InheritsTransform =
        Attributes.defineAvaloniaPropertyWithEquality Popup.InheritsTransformProperty

    let IsOpen = Attributes.defineAvaloniaPropertyWithEquality Popup.IsOpenProperty

    let PlacementAnchor =
        Attributes.defineAvaloniaPropertyWithEquality Popup.PlacementAnchorProperty

    let PlacementConstraintAdjustment =
        Attributes.defineAvaloniaPropertyWithEquality Popup.PlacementConstraintAdjustmentProperty

    let PlacementGravity =
        Attributes.defineAvaloniaPropertyWithEquality Popup.PlacementGravityProperty

    let Placement =
        Attributes.defineAvaloniaPropertyWithEquality Popup.PlacementProperty

    let PlacementRect =
        Attributes.defineAvaloniaPropertyWithEquality Popup.PlacementRectProperty

    let OverlayDismissEventPassThrough =
        Attributes.defineAvaloniaPropertyWithEquality Popup.OverlayDismissEventPassThroughProperty

    let HorizontalOffset =
        Attributes.defineAvaloniaPropertyWithEquality Popup.HorizontalOffsetProperty

    let IsLightDismissEnabled =
        Attributes.defineAvaloniaPropertyWithEquality Popup.IsLightDismissEnabledProperty

    let VerticalOffset =
        Attributes.defineAvaloniaPropertyWithEquality Popup.VerticalOffsetProperty

    let Topmost = Attributes.defineAvaloniaPropertyWithEquality Popup.TopmostProperty

    let OverlayInputPassThroughElement =
        Attributes.defineAvaloniaPropertyWidget Popup.OverlayInputPassThroughElementProperty

    let Closed =
        Attributes.defineEvent "Popup_Closed" (fun target -> (target :?> Popup).Closed)

    let Opened =
        Attributes.defineEventNoArg "Popup_Opened" (fun target -> (target :?> Popup).Opened)

[<AutoOpen>]
module PopupBuilders =
    type Fabulous.Avalonia.View with

        static member Popup(isOpen: bool, content: WidgetBuilder<'msg, #IFabControl>) =
            WidgetBuilder<'msg, IFabPopup>(
                Popup.WidgetKey,
                AttributesBundle(StackList.one(Popup.IsOpen.WithValue(isOpen)), ValueSome [| Popup.Child.WithValue(content.Compile()) |], ValueNone)
            )

[<Extension>]
type PopupModifiers =
    [<Extension>]
    static member inline windowManagerAddShadowHint(this: WidgetBuilder<'msg, #IFabPopup>, value: bool) =
        this.AddScalar(Popup.WindowManagerAddShadowHint.WithValue(value))

    [<Extension>]
    static member inline inheritsTransform(this: WidgetBuilder<'msg, #IFabPopup>, value: bool) =
        this.AddScalar(Popup.InheritsTransform.WithValue(value))

    [<Extension>]
    static member inline placementAnchor(this: WidgetBuilder<'msg, #IFabPopup>, value: PopupAnchor) =
        this.AddScalar(Popup.PlacementAnchor.WithValue(value))

    [<Extension>]
    static member inline placementConstraintAdjustment(this: WidgetBuilder<'msg, #IFabPopup>, value: PopupPositionerConstraintAdjustment) =
        this.AddScalar(Popup.PlacementConstraintAdjustment.WithValue(value))

    [<Extension>]
    static member inline placementGravity(this: WidgetBuilder<'msg, #IFabPopup>, value: PopupGravity) =
        this.AddScalar(Popup.PlacementGravity.WithValue(value))

    [<Extension>]
    static member inline placement(this: WidgetBuilder<'msg, #IFabPopup>, value: PlacementMode) =
        this.AddScalar(Popup.Placement.WithValue(value))

    [<Extension>]
    static member inline placementRect(this: WidgetBuilder<'msg, #IFabPopup>, value: Rect) =
        this.AddScalar(Popup.PlacementRect.WithValue(value))

    [<Extension>]
    static member inline overlayDismissEventPassThrough(this: WidgetBuilder<'msg, #IFabPopup>, value: bool) =
        this.AddScalar(Popup.OverlayDismissEventPassThrough.WithValue(value))

    [<Extension>]
    static member inline horizontalOffset(this: WidgetBuilder<'msg, #IFabPopup>, value: double) =
        this.AddScalar(Popup.HorizontalOffset.WithValue(value))

    [<Extension>]
    static member inline isLightDismissEnabled(this: WidgetBuilder<'msg, #IFabPopup>, value: bool) =
        this.AddScalar(Popup.IsLightDismissEnabled.WithValue(value))

    [<Extension>]
    static member inline verticalOffset(this: WidgetBuilder<'msg, #IFabPopup>, value: double) =
        this.AddScalar(Popup.VerticalOffset.WithValue(value))

    [<Extension>]
    static member inline topmost(this: WidgetBuilder<'msg, #IFabPopup>, value: bool) =
        this.AddScalar(Popup.Topmost.WithValue(value))

    [<Extension>]
    static member inline overlayInputPassThroughElement(this: WidgetBuilder<'msg, #IFabPopup>, content: WidgetBuilder<'mag, #IFabInputElement>) =
        this.AddWidget(Popup.OverlayInputPassThroughElement.WithValue(content.Compile()))

    [<Extension>]
    static member inline onClosed(this: WidgetBuilder<'msg, #IFabPopup>, onClosed: 'msg) =
        this.AddScalar(Popup.Closed.WithValue(fun _ -> onClosed |> box))

    [<Extension>]
    static member inline onOpened(this: WidgetBuilder<'msg, #IFabPopup>, onOpened: 'msg) =
        this.AddScalar(Popup.Opened.WithValue(onOpened))
