using System;
using System.Collections.Generic;

public class EventDispatcher
{
    /// <summary>
    /// �޼��� ���Ű (EventDispatcher enum ��� ����� ��� * �ߺ� �޼��� ��� ����))
    /// </summary>
    public enum EventName
    {
        UINPCPopupUpdate,
        UINPCPopupActive,
        MainCameraControllerHitEffects,
        SanctuarySceneMainIntotheDungeon,
        DungeonMainToNextStage,
        DungeonMainPlayerToNextRoom,
        DungeonMainPlayerToSanctuary,
        DungeonSceneMainPlayerExpUp,
        UIInventoryAddCell,
        UIInventoryAddEquipment,
        UICurrentInventoryList,
        UIGameOverPopUp,
        UIDungeonDirectorUISetOff,
        UIPortalArrowControllerInitializingArrows,
        UIPortalArrowControllerStopAllArrowCorutines,
        UIDialogPanelRandomWeaponDialog,
        UIDialogPanelStartDialog,
        UIShopGoodsCurrentGold,
        ChestItemGeneratorMakeChest,
        ChestItemGeneratorMakeItemForChest,
        ChestItemGeneratorMakeItemForInventory,
        ChestItemGeneratorMakeFieldCoin,
        DungeonSceneMainTakeFood,
        DungeonSceneMainTakeGun,
        PlayerShellTakeRelic,
        DungeonSceneMainTakeChestDamage,
        UIRelicDirectorTakeRelic,
        UICurrencyDirectorUpdateGoldUI,
        UICurrencyDirectorUpdateEtherUI,
        UIJoystickDirectorStopJoyStick,
        UIJoystickDirectorActiveJoyStick,
        UIAnnounceDirectorStartAnnounce,
        UIDungeonLoadingDirectorStageLoading,
        UIDungeonLoadingDirectorSanctuarytLoading,
        LaserLineInactivateLaser,
        LaserLineActivateLaser,
        UIInventoryDirectorMakeFieldFullText,
        UIInventoryDirectorMakeFullPopupText,
        UIInventoryDirectorMakeFieldFullHealthText,
        UIInventoryDirectorMakeHealthPopupText,
        UIInventoryDirectorButtonScaleAnim,
        UIFieldItemPopupDirectorUpdatePopup,
        UIFieldItemPopupDirectorClosePopup,
        PlayerDie,
        Test,//�׽�Ʈ�� �ӽ÷� �׽�Ʈ �Ͻð� ������ addlistner �ڵ� �����ּ���.
    }
    private static EventDispatcher instance;

    private Dictionary<EventName, Delegate> listeners = new Dictionary<EventName, Delegate>();

    public static EventDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EventDispatcher();
            }
            return instance;
        }
    }

    /// <summary>
    /// ��ȯ���� ���� �޼��� Ȥ�� �ν��Ͻ� �޼��� ��Ͻ�
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void AddListener(EventName eventName, Action listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            UnityEngine.Debug.LogWarning("<color=white>�̹� �ش� Ű�� �޼��尡 ��ϵ� �����Դϴ�. ���� �޼��带 ���� �������ϴ�.</color>");
            listeners.Remove(eventName);
            listeners.Add(eventName, listener);
        }
        else
        {
            listeners.Add(eventName, listener);
        }
    }

    /// <summary>
    /// ���� 1���� ������ ��ȯ���� ���� �޼��� Ȥ�� �ν��Ͻ� �޼��� ��Ͻ�
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void AddListener<T>(EventName eventName, Action<T> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            UnityEngine.Debug.LogWarning("<color=white>�̹� �ش� Ű�� �޼��尡 ��ϵ� �����Դϴ�. ���� �޼��带 ���� �������ϴ�.</color>");
            listeners.Remove(eventName);
            listeners.Add(eventName, listener);
        }
        else
        {
            listeners.Add(eventName, listener);
        }
    }

    /// <summary>
    /// ���� 2���� ������ ��ȯ���� ���� �޼��� Ȥ�� �ν��Ͻ� �޼��� ��Ͻ�
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void AddListener<T1, T2>(EventName eventName, Action<T1, T2> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            UnityEngine.Debug.LogWarning("<color=white>�̹� �ش� Ű�� �޼��尡 ��ϵ� �����Դϴ�. ���� �޼��带 ���� �������ϴ�.</color>");
            listeners.Remove(eventName);
            listeners.Add(eventName, listener);
        }
        else
        {
            listeners.Add(eventName, listener);
        }
    }

    /// <summary>
    /// ���� 3���� ������ ��ȯ���� ���� �޼��� Ȥ�� �ν��Ͻ� �޼��� ��Ͻ�
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="T3"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void AddListener<T1, T2, T3>(EventName eventName, Action<T1, T2, T3> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            UnityEngine.Debug.LogWarning("<color=white>�̹� �ش� Ű�� �޼��尡 ��ϵ� �����Դϴ�. ���� �޼��带 ���� �������ϴ�.</color>");
            listeners.Remove(eventName);
            listeners.Add(eventName, listener);
        }
        else
        {
            listeners.Add(eventName, listener);
        }
    }

    /// <summary>
    /// ��ȯ ���� �ִ� �޼��� ��Ͻ�
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void AddListener<TResult>(EventName eventName, Func<TResult> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            UnityEngine.Debug.LogWarning("<color=white>�̹� �ش� Ű�� �޼��尡 ��ϵ� �����Դϴ�. ���� �޼��带 ���� �������ϴ�.</color>");
            listeners.Remove(eventName);
            listeners.Add(eventName, listener);
        }
        else
        {
            listeners.Add(eventName, listener);
        }
    }

    /// <summary>
    /// ���� 1���� ������ ��ȯ ���� �ִ� �޼��� ��Ͻ�
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void AddListener<T1, TResult>(EventName eventName, Func<T1, TResult> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            UnityEngine.Debug.LogWarning("<color=white>�̹� �ش� Ű�� �޼��尡 ��ϵ� �����Դϴ�. ���� �޼��带 ���� �������ϴ�.</color>");
            listeners.Remove(eventName);
            listeners.Add(eventName, listener);
        }
        else
        {
            listeners.Add(eventName, listener);
        }
    }

    /// <summary>
    /// ���� 2���� ������ ��ȯ ���� �ִ� �޼��� ��Ͻ�
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void AddListener<T1, T2, TResult>(EventName eventName, Func<T1, T2, TResult> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            UnityEngine.Debug.LogWarning("<color=white>�̹� �ش� Ű�� �޼��尡 ��ϵ� �����Դϴ�. ���� �޼��带 ���� �������ϴ�.</color>");
            listeners.Remove(eventName);
            listeners.Add(eventName, listener);
        }
        else
        {
            listeners.Add(eventName, listener);
        }
    }

    /// <summary>
    /// ��ȯ���� ���� �޼��� ������
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void RemoveListener(EventName eventName, Action listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            listeners[eventName] = Delegate.Remove(listeners[eventName], listener);
            if (listeners[eventName] == null)
            {
                listeners.Remove(eventName);
            }
        }
    }

    /// <summary>
    /// ���� 1���� ������ ��ȯ���� ���� �޼��� ������
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void RemoveListener<T>(EventName eventName, Action<T> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            listeners[eventName] = Delegate.Remove(listeners[eventName], listener);
            if (listeners[eventName] == null)
            {
                listeners.Remove(eventName);
            }
        }
    }

    /// <summary>
    /// ���� 2���� ������ ��ȯ���� ���� �޼��� ������
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void RemoveListener<T1, T2>(EventName eventName, Action<T1, T2> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            listeners[eventName] = Delegate.Remove(listeners[eventName], listener);
            if (listeners[eventName] == null)
            {
                listeners.Remove(eventName);
            }
        }
    }
    /// <summary>
    /// ��ȯ ���� �ִ� �޼��� ���Ž�
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void RemoveListener<TResult>(EventName eventName, Func<TResult> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            listeners[eventName] = Delegate.Remove(listeners[eventName], listener);
            if (listeners[eventName] == null)
            {
                listeners.Remove(eventName);
            }
        }
    }

    /// <summary>
    /// ���� 1���� ������ ��ȯ ���� �ִ� �޼��� ���Ž�
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void RemoveListener<T1, TResult>(EventName eventName, Func<T1, TResult> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            listeners[eventName] = Delegate.Remove(listeners[eventName], listener);
            if (listeners[eventName] == null)
            {
                listeners.Remove(eventName);
            }
        }
    }
    /// <summary>
    /// ���� 2���� ������ ��ȯ ���� �ִ� �޼��� ���Ž�
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public void RemoveListener<T1, T2, TResult>(EventName eventName, Func<T1, T2, TResult> listener)
    {
        if (listeners.ContainsKey(eventName))
        {
            listeners[eventName] = Delegate.Remove(listeners[eventName], listener);
            if (listeners[eventName] == null)
            {
                listeners.Remove(eventName);
            }
        }
    }

    /// <summary>
    /// ��ȯ���� ���� �޼��� Ȥ�� �ν��Ͻ� �޼��� ����
    /// </summary>
    /// <param name="eventName"></param>
    public void Dispatch(EventName eventName)
    {
        if (listeners.ContainsKey(eventName))
        {
            ((Action)listeners[eventName])();
        }
    }

    /// <summary>
    /// ��ȯ���� ���� �޼��� Ȥ�� �ν��Ͻ� �޼��� ����
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="eventParams"></param>
    /// <param name="eventParams2"></param>
    public void Dispatch<T>(EventName eventName, T eventParams = default)
    {
        if (listeners.ContainsKey(eventName))
        {
            ((Action<T>)listeners[eventName])(eventParams);
        }
    }

    /// <summary>
    /// ��ȯ���� ���� �޼��� Ȥ�� �ν��Ͻ� �޼��� ����
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="eventParams"></param>
    /// <param name="eventParams2"></param>
    public void Dispatch<T1, T2>(EventName eventName, T1 eventParams = default, T2 eventParams2 = default)
    {
        if (listeners.ContainsKey(eventName))
        {
            ((Action<T1, T2>)listeners[eventName])(eventParams, eventParams2);
        }
    }

    /// <summary>
    /// ��ȯ���� ���� �޼��� Ȥ�� �ν��Ͻ� �޼��� ����
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="eventName"></param>
    /// <param name="eventParams"></param>
    /// <param name="eventParams2"></param>
    public void Dispatch<T1, T2, T3>(EventName eventName, T1 eventParams = default, T2 eventParams2 = default, T3 eventParams3 = default)
    {
        if (listeners.ContainsKey(eventName))
        {
            ((Action<T1, T2, T3>)listeners[eventName])(eventParams, eventParams2, eventParams3);
        }
    }

    /// <summary>
    /// ��ȯ ���� �ִ� �޼��� Ȥ�� �ν��Ͻ� �޼��� ����
    /// </summary>
    /// <typeparam name="TResult"> ��ȯ Ÿ��</typeparam>
    /// <param name="eventName">�̺�Ʈ ��� Ű</param>
    /// <param name="result">��ȯ��</param>
    /// <exception cref="InvalidCastException"></exception>
    public void Dispatch<TResult>(EventName eventName, out TResult result)
    {
        if (listeners.ContainsKey(eventName))
        {
            var listener = listeners[eventName];
            if (listener is Func<TResult> func)
            {
                result = func();
            }
            else
            {
                throw new InvalidCastException($"Listener for event '{eventName}' is not a Func with the correct signature.");
            }
        }
        else
        {
            result = default;
        }
    }

    /// <summary>
    /// ���ڸ� 1�� �ް��ȯ ���� �ִ� �޼��� Ȥ�� �ν��Ͻ� �޼��� ����
    /// </summary>
    /// <typeparam name="TResult"> ��ȯ Ÿ��</typeparam>
    /// <param name="eventName">�̺�Ʈ ��� Ű</param>
    /// <param name="eventParams">�Ű�����</param>
    /// <param name="result">��ȯ��</param>
    /// <exception cref="InvalidCastException"></exception>
    public void Dispatch<T, TResult>(EventName eventName, T eventParams, out TResult result)
    {
        if (listeners.ContainsKey(eventName))
        {
            var listener = listeners[eventName];
            if (listener is Func<T, TResult> func)
            {
                result = func(eventParams);
            }
            else
            {
                throw new InvalidCastException($"Listener for event '{eventName}' is not a Func with the correct signature.");
            }
        }
        else
        {
            result = default;
        }
    }

    /// <summary>
    /// ���ڸ� 2�� �ް��ȯ ���� �ִ� �޼��� Ȥ�� �ν��Ͻ� �޼��� ����
    /// </summary>
    /// <typeparam name="TResult"> ��ȯ Ÿ��</typeparam>
    /// <param name="eventName">�̺�Ʈ ��� Ű</param>
    /// <param name="eventParams">ù��° ����</param>
    /// <param name="eventParams2">�ι�° ����</param>
    /// <param name="result">��ȯ��</param>
    /// <exception cref="InvalidCastException"></exception>
    public void Dispatch<T1, T2, TResult>(EventName eventName, T1 eventParams, T2 eventParams2, out TResult result)
    {
        if (listeners.ContainsKey(eventName))
        {
            var listener = listeners[eventName];
            if (listener is Func<T1, T2, TResult> func)
            {
                result = func(eventParams, eventParams2);
            }
            else
            {
                throw new InvalidCastException($"Listener for event '{eventName}' is not a Func with the correct signature.");
            }
        }
        else
        {
            result = default;
        }
    }


}