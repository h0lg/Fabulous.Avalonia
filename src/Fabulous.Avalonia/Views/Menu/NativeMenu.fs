namespace Fabulous.Avalonia

open System.Runtime.CompilerServices
open Avalonia.Controls
open Fabulous
open Fabulous.StackAllocatedCollections

module NativeMenu =
    let WidgetKey = Widgets.register<NativeMenu>()

    let Items =
        Attributes.defineListWidgetCollection "NativeMenu_Items" (fun target -> (target :?> NativeMenu).Items)

    let Opening =
        Attributes.defineEvent "NativeMenu_Opening" (fun target -> (target :?> NativeMenu).Opening)

    let Closed =
        Attributes.defineEvent "NativeMenu_Opening" (fun target -> (target :?> NativeMenu).Closed)

    let NeedsUpdate =
        Attributes.defineEvent "NativeMenu_NeedsUpdate" (fun target -> (target :?> NativeMenu).NeedsUpdate)

module NativeMenuAttached =
    let NativeMenu = Attributes.defineAvaloniaPropertyWidget NativeMenu.MenuProperty

    let IsNativeMenuExported =
        Attributes.defineAvaloniaPropertyWithEquality Avalonia.Controls.NativeMenu.IsNativeMenuExportedProperty

[<AutoOpen>]
module NativeMenuBuilders =
    type Fabulous.Avalonia.View with

        static member inline NativeMenu() =
            CollectionBuilder<'msg, IFabNativeMenu, IFabNativeMenuItem>(NativeMenu.WidgetKey, NativeMenu.Items)

[<Extension>]
type NativeMenuModifiers =
    [<Extension>]
    static member inline onOpening(this: WidgetBuilder<'msg, #IFabNativeMenu>, onOpening: 'msg) =
        this.AddScalar(NativeMenu.Opening.WithValue(fun _ -> onOpening |> box))

    [<Extension>]
    static member inline onClosed(this: WidgetBuilder<'msg, #IFabNativeMenu>, onClosed: 'msg) =
        this.AddScalar(NativeMenu.Closed.WithValue(fun _ -> onClosed |> box))

    [<Extension>]
    static member inline onNeedsUpdate(this: WidgetBuilder<'msg, #IFabNativeMenu>, onNeedsUpdate: 'msg) =
        this.AddScalar(NativeMenu.NeedsUpdate.WithValue(fun _ -> onNeedsUpdate |> box))

    /// <summary>Link a ViewRef to access the direct NativeMenu control instance</summary>
    /// <param name="this">Current widget</param>
    /// <param name="value">The ViewRef instance that will receive access to the underlying control</param>
    [<Extension>]
    static member inline reference(this: WidgetBuilder<'msg, IFabNativeMenu>, value: ViewRef<NativeMenu>) =
        this.AddScalar(ViewRefAttributes.ViewRef.WithValue(value.Unbox))

[<Extension>]
type NativeMenuAttachedModifiers =
    [<Extension>]
    static member inline menu(this: WidgetBuilder<'msg, #IFabWindow>, menu: WidgetBuilder<'msg, #IFabNativeMenu>) =
        this.AddWidget(NativeMenuAttached.NativeMenu.WithValue(menu.Compile()))

    [<Extension>]
    static member inline isNativeMenuExported(this: WidgetBuilder<'msg, #IFabTopLevel>, value: bool) =
        this.AddScalar(NativeMenuAttached.IsNativeMenuExported.WithValue(value))

[<Extension>]
type NativeViewYieldExtensions =
    [<Extension>]
    static member inline Yield(_: CollectionBuilder<'msg, #IFabNativeMenu, IFabNativeMenuItem>, x: WidgetBuilder<'msg, #IFabNativeMenuItem>) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }

    [<Extension>]
    static member inline Yield
        (
            _: CollectionBuilder<'msg, #IFabNativeMenu, IFabNativeMenuItem>,
            x: WidgetBuilder<'msg, Memo.Memoized<#IFabNativeMenuItem>>
        ) : Content<'msg> =
        { Widgets = MutStackArray1.One(x.Compile()) }
