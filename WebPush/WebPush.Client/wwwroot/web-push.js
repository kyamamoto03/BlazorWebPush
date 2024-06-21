// ServiceWorkerの登録
export function serviceWorkRegister() {
    navigator.serviceWorker.register('_content/WebPush.Client/service-worker.js');
}

// WebPush通知の購読
export async function requestSubscription(publicKey) {
    //ログ出力
    console.log('requestSubscription');

    const worker = await navigator.serviceWorker.getRegistration('_content/WebPush.Client/service-worker.js');
    worker.update();
    console.log(worker);

    const existingSubscription = await worker.pushManager.getSubscription();
    if (!existingSubscription) {
        const newSubscription = await subscribe(worker, publicKey);
        if (newSubscription) {
            console.log("succes:" + newSubscription);
            return {
                url: newSubscription.endpoint,
                p256dh: arrayBufferToBase64(newSubscription.getKey('p256dh')),
                auth: arrayBufferToBase64(newSubscription.getKey('auth'))
            };
        } else {
            console.log("newSubscription FAILED");

        }
    } else {
        console.log("existingSubscription FAILED");
    }
}

async function subscribe(worker, publicKey) {
    try {
        return await worker.pushManager.subscribe({
            userVisibleOnly: true,
            applicationServerKey: publicKey
        });
    } catch (error) {
        if (error.name === 'NotAllowedError') {
            return null;
        }
        throw error;
    }
}

function arrayBufferToBase64(buffer) {
    // https://stackoverflow.com/a/9458996
    var binary = '';
    var bytes = new Uint8Array(buffer);
    var len = bytes.byteLength;
    for (var i = 0; i < len; i++) {
        binary += String.fromCharCode(bytes[i]);
    }
    return window.btoa(binary);
}
