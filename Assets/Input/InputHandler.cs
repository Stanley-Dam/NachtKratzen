// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputHandler.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputHandler : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputHandler()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputHandler"",
    ""maps"": [
        {
            ""name"": ""movement"",
            ""id"": ""484ec9b8-ebe6-4e16-ad29-a1189b41e00e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""9ac34187-ae28-400b-b88b-b5d2da436982"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""886daa14-a124-4fd8-a965-7360abf1498b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""dab0e40a-606e-4451-b716-840365a9370f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseLook"",
                    ""type"": ""Value"",
                    ""id"": ""66a0fb05-dda3-46b2-ba72-c487f9ef26b8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Directions"",
                    ""id"": ""dad90c33-7415-49b5-8960-a7c7d3b1cd20"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""0221cd41-851f-4285-b2a6-ca950c3cd5e9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""82e0605b-2c38-45e5-8d4b-e66360d9313d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""05bf9f42-f955-433c-ade1-90d213a49ec9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3472098c-8cc5-424f-b35e-1ef0fa94f33b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b985c516-40b0-43c7-89b3-835b495d7634"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9328a71-705f-4b67-b18e-c9dbee658d45"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5288d3a0-6a4e-426f-82a3-a8a8b41dabc9"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseLook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""interact"",
            ""id"": ""4122859a-bdf9-472f-9e81-c97a55b8c3e2"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""e9997078-e1cb-4c8d-9e0d-22e03fe102da"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b2db2b81-c8ae-4893-805d-e7f9817c270d"",
                    ""path"": ""<Mouse>/clickCount"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""pickup"",
            ""id"": ""7e7940ff-4c05-41a9-b17a-724596ddd8b8"",
            ""actions"": [
                {
                    ""name"": ""interact"",
                    ""type"": ""Button"",
                    ""id"": ""59c4292f-71af-43f2-bc7f-2a4749d4d370"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3ea5ea41-9218-40a8-8690-f3fa738d0a4e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // movement
        m_movement = asset.FindActionMap("movement", throwIfNotFound: true);
        m_movement_Move = m_movement.FindAction("Move", throwIfNotFound: true);
        m_movement_Sprint = m_movement.FindAction("Sprint", throwIfNotFound: true);
        m_movement_Jump = m_movement.FindAction("Jump", throwIfNotFound: true);
        m_movement_MouseLook = m_movement.FindAction("MouseLook", throwIfNotFound: true);
        // interact
        m_interact = asset.FindActionMap("interact", throwIfNotFound: true);
        m_interact_Click = m_interact.FindAction("Click", throwIfNotFound: true);
        // pickup
        m_pickup = asset.FindActionMap("pickup", throwIfNotFound: true);
        m_pickup_interact = m_pickup.FindAction("interact", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // movement
    private readonly InputActionMap m_movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_movement_Move;
    private readonly InputAction m_movement_Sprint;
    private readonly InputAction m_movement_Jump;
    private readonly InputAction m_movement_MouseLook;
    public struct MovementActions
    {
        private @InputHandler m_Wrapper;
        public MovementActions(@InputHandler wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_movement_Move;
        public InputAction @Sprint => m_Wrapper.m_movement_Sprint;
        public InputAction @Jump => m_Wrapper.m_movement_Jump;
        public InputAction @MouseLook => m_Wrapper.m_movement_MouseLook;
        public InputActionMap Get() { return m_Wrapper.m_movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Sprint.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSprint;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @MouseLook.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseLook;
                @MouseLook.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseLook;
                @MouseLook.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMouseLook;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @MouseLook.started += instance.OnMouseLook;
                @MouseLook.performed += instance.OnMouseLook;
                @MouseLook.canceled += instance.OnMouseLook;
            }
        }
    }
    public MovementActions @movement => new MovementActions(this);

    // interact
    private readonly InputActionMap m_interact;
    private IInteractActions m_InteractActionsCallbackInterface;
    private readonly InputAction m_interact_Click;
    public struct InteractActions
    {
        private @InputHandler m_Wrapper;
        public InteractActions(@InputHandler wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_interact_Click;
        public InputActionMap Get() { return m_Wrapper.m_interact; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractActions set) { return set.Get(); }
        public void SetCallbacks(IInteractActions instance)
        {
            if (m_Wrapper.m_InteractActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_InteractActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_InteractActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_InteractActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_InteractActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }
        }
    }
    public InteractActions @interact => new InteractActions(this);

    // pickup
    private readonly InputActionMap m_pickup;
    private IPickupActions m_PickupActionsCallbackInterface;
    private readonly InputAction m_pickup_interact;
    public struct PickupActions
    {
        private @InputHandler m_Wrapper;
        public PickupActions(@InputHandler wrapper) { m_Wrapper = wrapper; }
        public InputAction @interact => m_Wrapper.m_pickup_interact;
        public InputActionMap Get() { return m_Wrapper.m_pickup; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PickupActions set) { return set.Get(); }
        public void SetCallbacks(IPickupActions instance)
        {
            if (m_Wrapper.m_PickupActionsCallbackInterface != null)
            {
                @interact.started -= m_Wrapper.m_PickupActionsCallbackInterface.OnInteract;
                @interact.performed -= m_Wrapper.m_PickupActionsCallbackInterface.OnInteract;
                @interact.canceled -= m_Wrapper.m_PickupActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_PickupActionsCallbackInterface = instance;
            if (instance != null)
            {
                @interact.started += instance.OnInteract;
                @interact.performed += instance.OnInteract;
                @interact.canceled += instance.OnInteract;
            }
        }
    }
    public PickupActions @pickup => new PickupActions(this);
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMouseLook(InputAction.CallbackContext context);
    }
    public interface IInteractActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
    public interface IPickupActions
    {
        void OnInteract(InputAction.CallbackContext context);
    }
}
