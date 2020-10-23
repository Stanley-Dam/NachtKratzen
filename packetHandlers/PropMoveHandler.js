const Prop = require("../obj/Prop.js");

function PropMoveHandler(server, data) {
    var prop = server.GetPropById(data.objectId);
    
    if(prop == null) {
        //This happens when the prop hasn't been moved before

        prop = new Prop(data.objectId, data.isBeingHeld, data.clientIdFrom, data.locationToX, data.locationToY, data.locationToZ, data.rotationToX, data.rotationToY, data.rotationToZ, data.rotationToW);
        server.props.push(prop);
        server.BroadCastToClients('PropMove', data);

        return;
    }

    prop.isBeingHeld = data.isBeingHeld;
    prop.clientIdFrom = data.clientIdFrom;

    prop.x = data.locationToX;
    prop.y = data.locationToY;
    prop.z = data.locationToZ;

    prop.rotationX = data.rotationToX;
    prop.rotationY = data.rotationToY;
    prop.rotationZ = data.rotationToZ;
    prop.rotationW = data.rotationToW;

    server.BroadCastToClients('PropMove', data);
}

module.exports = PropMoveHandler;