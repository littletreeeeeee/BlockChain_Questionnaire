const MiaoToken = artifacts.require("MiaoToken");

module.exports = function (deployer) {
    deployer.deploy(MiaoToken, { from:"0x2F70eB4B99201d48B3C4335Fc082cE1E510F3CF3"});
};1