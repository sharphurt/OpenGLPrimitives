#version 430 core

uniform ivec2 viewportDimensions;
//uniform mat4 gl_ProjectionMatrix;
uniform ivec4 viewport;

out vec3 color;

struct Ray {
    vec3 origin;
    vec3 direction;
};

struct Sphere {
    vec3 origin;
    float radius;
};

float zNear = -0.1f;
float zFar = -100.0f;
float fieldOfViewX = 3.1415926535897932384626433832795 / 2.0f;

float sampleRay(Ray ray,Sphere s, float distance);
Ray computeEyeRay(float, float, int, int);
bool intersect(Ray r, Sphere s);
bool solveQuadratic(float a, float b, float c);
Ray calcEyeFromWindow(vec3 windowSpace);

void main() {
//  Ray r = computeEyeRay(gl_FragCoord.x + 0.5f, gl_FragCoord.y + 0.5f, viewportDimensions.x, viewportDimensions.y);
    Ray r = calcEyeFromWindow(vec3(gl_FragCoord.x + 0.5f, gl_FragCoord.y + 0.5f, 1));
    Sphere s;
    s.origin = vec3(0.5f,-0.5f,-0.7f);
    s.radius = 0.8f;

    if (intersect(r,s))
        color = vec3(1,1,1);
    else
        color = vec3(0,0,0);
    }

Ray calcEyeFromWindow(vec3 windowSpace)
{
    vec4 ndcPos;
    ndcPos.xy = ((2.0 * gl_FragCoord.xy) - (2.0 * viewport.xy)) / (viewport.zw) - 1;
    ndcPos.z = (2.0 * gl_FragCoord.z - gl_DepthRange.near - gl_DepthRange.far) /
    (gl_DepthRange.far - gl_DepthRange.near);
    ndcPos.w = 1.0;

    vec4 clipPos = ndcPos / gl_FragCoord.w;
    vec4 eyePos = inverse(gl_ProjectionMatrix) * clipPos;

    Ray r;
    r.origin = eyePos.xyz;
    r.direction =normalize(eyePos.xyz);

    return r;
}

Ray computeEyeRay(float x, float y, int width, int height) {
    const float aspect = float(height) / float(width);
    const float s = -2.0f * tan(fieldOfViewX * 0.5f);

    const vec3 start = vec3( (float(x) / float(width) - 0.5) * s,
                        -(float(y) / float(height) - 0.5f) * s * aspect,
                        1.0f) * zNear;
    float startLength = sqrt( (start.x * start.x) + (start.y * start.y) + (start.z * start.z) );

    Ray e;
    e.origin = start;
    e.direction = normalize(start);
    return e;
}

bool intersect(Ray r, Sphere s) {
    float a = dot(r.direction,r.direction);
    float b = dot(r.direction, 2.0 * (r.origin-s.origin));
    float c = dot(s.origin, s.origin) + dot(r.origin,r.origin) +-2.0*dot(r.origin,s.origin) - (s.radius*s.radius);

    float disc = b*b + (-4.0)*a*c;

    if (disc < 0)
        return false;

    return true;

}


bool solveQuadratic(float a, float b, float c) {
    float t, t_1;

    float disc = a * b - 4 * a * c;
    if (disc < 0) 
        return false;
    else {
        if (disc == 0)
            t = -0.5 * b / a;
        else {
            float q = (b > 0) ? -0.5 * (b+sqrt(disc)) : -0.5 * (b-sqrt(disc));
            t = q / a;
            t_1 = c / q;
        }
    }
    return true;

}