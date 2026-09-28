shader_type canvas_item;

// 磨砂质感参数
uniform float roughness = 0.06;      // 粗糙度
uniform float grain_density = 120.0; // 颗粒密度

void fragment() {
    vec2 uv = UV;
    
    // 1. 获取原始颜色
    vec4 color = texture(TEXTURE, uv);
    color *= COLOR;
    
    // 2. 渐变透明度
    float alpha = 1.0 - uv.y;
    
    // 3. 简单磨砂质感
    // 创建随机噪声
    float random = fract(sin(dot(uv * grain_density, vec2(12.9898, 78.233))) * 43758.5453);
    
    // 将噪声映射到 [-roughness, roughness] 范围
    float grain = (random * 2.0 - 1.0) * roughness;
    
    // 应用到颜色（同时影响RGB通道，模拟磨砂反射）
    color.rgb += grain;
    
    // 4. 应用透明度渐变
    color.a *= alpha;
    
    COLOR = color;
}
